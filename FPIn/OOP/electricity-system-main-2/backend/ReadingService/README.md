## Структура ReadingService
```text
ReadingService/
│
├── Controllers/                         # API-контроллеры микросервиса
│   └── ReadingSubmissionController.cs    # Endpoints для передачи, получения и изменения статуса показаний
│
├── DTOs/                                # DTO-классы для запросов и ответов API
│   ├── Common/                          # Общие DTO
│   │   └── ApiErrorResponse.cs           # Единый формат ошибки API
│   │
│   └── Readings/                        # DTO для работы с показаниями
│       ├── CreateReadingSubmissionRequest.cs       # Запрос на передачу показаний
│       ├── MeterReadingResponse.cs                 # Ответ с данными одного показания
│       ├── ReadingItemRequest.cs                   # Одно показание по конкретному прибору учета
│       ├── ReadingSubmissionResponse.cs            # Ответ с полной информацией о передаче показаний
│       └── UpdateReadingSubmissionStatusRequest.cs # Запрос на изменение статуса показаний
│
├── Enums/                               # Перечисления микросервиса
│   └── ReadingSubmissionStatus.cs        # Статусы показаний: Submitted, Accepted, Rejected
│
├── Models/                              # Будущие доменные модели сервиса
│
├── Services/                            # Будущая бизнес-логика сервиса
│
├── appsettings.json                     # Конфигурация сервиса
├── Program.cs                           # Точка входа и настройка приложения
└── ReadingService.csproj                # Файл проекта .NET
```