# Лабораторная работа 2

## Написание Docker

Разработка Docker будет проводиться на основе альфа-версии Backend сервиса приложения Smart Receipt. 

Используемый стек сервиса - Java 22, Spring Boot 4.0.0 RC2.0, Maven. 

Ссылка на [репозиторий](https://github.com/raf-me/ITMO/tree/main/FPIn/DemoBackendProject) 

#### Структура Backend:

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

### Плохой Dockerfile:

    FROM openjdk:latest
    WORKDIR /app
    COPY . .
    RUN mvn clean package
    EXPOSE 8080
    CMD ["java", "-jar", "target/DemoBackendProject-0.0.1-SNAPSHOT.jar"]

### Использование openjdk:latest

    FROM openjdk:latest

При каждом новом билде Docker может использовать другую версию базового образа, что может привести к:

- неожиданным ошибкам,
- несовместимости библиотек,
- различному поведению контейнера на разных машинах и в разное время.

Кроме того, полный образ Java содержит большое количество компонентов, не необходимых для запуска приложения в production, что увеличивает размер образа и площадь атаки.

### Сборка и запуск приложения в одном образе

    RUN mvn clean package
    CMD ["java", "-jar", "target/DemoBackendProject-0.0.1-SNAPSHOT.jar"]

В одном образе одновременно находятся:

- Maven,
- исходный код,
- build-зависимости,
- временные файлы сборки.

Это приводит к:

- увеличению размера итогового образа,
- более медленной сборке,
- нарушению принципа разделения ответственности.
