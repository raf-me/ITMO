## Структура ContractService
```text
ContractService/
│
├── Controllers/                          # API-контроллеры микросервиса
│   ├── ContractsController.cs             # Создание, получение и изменение статуса договоров
│   ├── DocumentsController.cs             # Добавление и получение документов
│   ├── MetersController.cs                # Создание и получение приборов учета
│   └── OrganizationsController.cs         # Создание и получение организаций
│
├── Data/                                  # Работа с базой данных
│   └── ContractDbContext.cs               # DbContext сервиса и настройка таблиц, связей и индексов
│
├── DTOs/                                  # DTO-классы для запросов и ответов API
│   ├── Common/                            # Общие DTO
│   │   └── ApiErrorResponse.cs            # Единый формат ошибки API
│   │
│   ├── Contracts/                         # DTO для работы с договорами
│   │   ├── ContractResponse.cs            # Ответ с данными договора
│   │   ├── CreateContractRequest.cs       # Запрос на создание договора
│   │   └── UpdateContractStatusRequest.cs # Запрос на изменение статуса договора
│   │
│   ├── Documents/                         # DTO для работы с документами
│   │   ├── CreateDocumentRequest.cs       # Запрос на добавление документа
│   │   └── DocumentResponse.cs            # Ответ с данными документа
│   │
│   ├── Meters/                            # DTO для работы с приборами учета
│   │   ├── CreateMeterRequest.cs          # Запрос на создание прибора учета
│   │   ├── InternalMeterResponse.cs       # Ответ для внутренней проверки прибора ReadingService
│   │   └── MeterResponse.cs               # Ответ с данными прибора учета
│   │
│   └── Organizations/                     # DTO для работы с организациями
│       ├── CreateOrganizationRequest.cs   # Запрос на создание организации
│       └── OrganizationResponse.cs        # Ответ с данными организации
│
├── Enums/                                 # Перечисления микросервиса
│   ├── ContractStatus.cs                  # Статусы договора: Draft, Active, Archived
│   ├── DocumentType.cs                    # Тип документа: ContractScan
│   └── MeterType.cs                       # Тип прибора учета: SingleTariff, TwoTariff
│
├── Migrations/                            # Миграции Entity Framework Core
│   ├── ..._InitialCreate.cs               # Первая миграция для создания таблиц
│   └── ContractDbContextModelSnapshot.cs  # Снимок текущей модели БД
│
├── Models/                                # Доменные модели микросервиса
│   ├── AuditLog.cs                        # Лог действий администратора
│   ├── Contract.cs                        # Модель договора
│   ├── Document.cs                        # Модель документа договора
│   ├── Meter.cs                           # Модель прибора учета
│   └── Organization.cs                    # Модель организации-клиента
│
├── Services/                              # Бизнес-логика микросервиса
│   ├── ContractsService.cs                # Логика работы с договорами и аудитом изменения статуса
│   ├── DocumentsService.cs                # Логика работы с документами договоров
│   ├── MetersService.cs                   # Логика работы с приборами учета
│   └── OrganizationService.cs             # Логика работы с организациями
│
├── appsettings.json                       # Конфигурация сервиса и строка подключения к БД
├── ContractService.csproj                 # Файл проекта .NET
├── Program.cs                             # Точка входа, Swagger, DI и подключение DbContext
└── README.md                              # Описание микросервиса
```