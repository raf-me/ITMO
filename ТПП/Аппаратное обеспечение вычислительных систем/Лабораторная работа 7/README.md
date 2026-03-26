
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

