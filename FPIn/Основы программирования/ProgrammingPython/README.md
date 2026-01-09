
### Структура проекта

    src/
    └─ main/
        ├─ java/com/example/demobackendproject
        │  ├─ api/                 # Обёртки над внешними HTTP-API
        │  │   └─ ReceiptDecodeApi.java
        │  ├─ config/              # Java-конфигурация Spring
        │  │   ├─ SecurityConfig.java
        │  │   └─ WebClientConfig.java
        │  ├─ controller/          # REST-контроллеры
        │  │   └─ ReceiptController.java
        │  ├─ dto/                 # DTO для запросов/ответов
        │  │   ├─ CategoryDTO.java
        │  │   ├─ ReceiptItemDTO.java
        │  │   ├─ ReceiptQRRequestDTO.java
        │  │   ├─ ReceiptQrParseDTO.java
        │  │   ├─ ReceiptRequestDTO.java
        │  │   └─ ReceiptResponseDTO.java
        │  └─ service/
        │      └─ HTTP_DATA.java   # Бизнес-логика/оркестрация
        └─ resources/
        ├─ static/index.html    # Пробная страница "сервер работает"
        ├─ application.properties
        └─ test.http            # HTTP-сценарии для теста из IDE

---

### HTTP_DATA.java

Методы
##### String decodeRawQR(String qrString)

Что делает:
- вызывает decodeApi.decodeReceipt(...).block(...) и возвращает сырой ответ.

Исключения:
- При ответе внешнего API с ошибкой ловит WebClientResponseException и пробрасывает 502 Bad Gateway с телом ошибки.
- При неожиданных сбоях — 503 Service Unavailable.

Практика:
- пригодно для health-проверок и диагностики — сразу видишь, что отвечает внешка.

##### ReceiptResponseDTO processReceipt(ReceiptRequestDTO request)

Что делает сейчас:
- подготавливает пустой список позиций, кладёт отладочные данные (debugData), при наличии receiptId вызывает decode-API (мокается, если токен пустой), складывает всё в ReceiptResponseDTO.

Практика:
- это «скелет» бизнес-процесса. Сюда добавится парсинг сырого JSON чека → маппинг в items → рассчёт totalSum → категоризация по входному справочнику.

Отладка:
- поле debugData — быстрый способ видеть, что пришло от внешнего сервиса, не лезя в логи.

##### Map<String,String> parseQrCode(String qr)

Что делает:
- разбивает строку QR по & и =.

Практика: 
- полезно, чтобы быстро вытащить t, s, fn, i, fp, n.

---

### ReceiptDecodeApi.java
Пакет: com.example.demobackendproject.api

Назначение: один ответственный класс, где описываем, как звонить в внешнее decode-API.

    private WebClient webClient;
    
    @Value("${receipt.api.token}")
    private String apiToken;
    
    @Value("${receipt.api.url}")
    private String apiUrl;

##### Практика:
- apiUrl и apiToken берём из application.yml/переменных среды.
- Если токен пустой или “token”, метод возвращает мок: {"mock":true,"message":"external call skipped"}. Это позволяет локально разворачивать бэк без внешних зависимостей.

##### Метод
- Mono<String> decodeReceipt(String receiptId)
- Если токен отсутствует → вернуть мок (локальная разработка).
- Иначе сходить во внешку: GET ${apiUrl}?token=...&qr=...
- При HTTP-ошибках конвертирует их в ReceiptApiException (Runtime).

##### Практическая польза: 
- единая точка настройки URL/токенов, таймаутов, ретраев, логирования запросов/ответов. Когда поменяется внешний API — правим только здесь.

---

### ReceiptRequestDTO.java

    private String receiptId;
    private List<CategoryDTO> categories;

##### Назначение: 
- вход для /api/categorize.
##### Практика: 
- фронт присылает receiptId и актуальный справочник категорий (id+название+описание), чтобы бэк категоризировал товары в рамках этого справочника.

---

### ReceiptResponseDTO.java

    private String receiptId;
    private List<ReceiptItemDTO> items;
    private float totalSum;
    private List<CategoryDTO> categories;
    private Map<String, Object> debugData;

##### Назначение: 
- текущий «плоский» ответ бэка.
##### Практика: 
- удобно на ранней стадии — фронт видит, что пришло, а вы шагами наполняете items и totalSum.

---

### ReceiptItemDTO.java

    id, name, description

##### Назначение: 
- позиция чека.

##### Практика: 
- сюда маппятся конкретные товары из расшифрованного JSON. category выставляется в соответствии со справочником из запроса.

---

### ReceiptQRRequestDTO.java / ReceiptQrParseDTO.java

##### Назначение: 
- простые обёртки под тело POST /decode и /parseQr.

##### Практика: 
- даёт типобезопасность и понятную схему входа.

---

### WebClientConfig.java

    @Bean
        public WebClient webClient() {
            return WebClient.builder().build();
    }

##### Назначение: 
- единый WebClient в контексте.

##### Практика: 
- тут обычно настраивают baseUrl, таймауты, логирование, retry, кодеки. Пример:

        WebClient.builder()
            .baseUrl("https://decode.example/api")
            .clientConnector(new ReactorClientHttpConnector(HttpClient.create()
                .responseTimeout(Duration.ofSeconds(5))))
            .build();

---

### SecurityConfig.java

    http.csrf(csrf -> csrf.disable())
        .authorizeHttpRequests(auth -> auth
            .requestMatchers("/api/**").permitAll()
            .anyRequest().permitAll());

##### Назначение: 
- открыть ваши API без аутентификации на время разработки.

##### Практика: 
- для продакшена поменять политику: закрыть всё кроме нужного, добавить CORS и т.д.

---




