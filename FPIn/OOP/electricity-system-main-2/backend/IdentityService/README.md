## Структура IdentityService
```text
IdentityService/
│
├── Controllers/                          # API-контроллеры: принимают HTTP-запросы
│   ├── AuthController.cs                  # Регистрация, вход, подтверждение 2FA
│   ├── RolesController.cs                 # Получение списка ролей
│   └── UsersController.cs                 # Работа с пользователями и их статусами
│
├── Data/                                  # Работа с базой данных
│   └── IdentityDBContext.cs               # DbContext для таблиц User и Role
│
├── DTOs/                                  # DTO-классы для API-запросов и ответов
│   ├── Auth/                              # DTO для регистрации, входа и 2FA
│   │   ├── RegisterRequest.cs             # Данные для регистрации пользователя
│   │   ├── RegisterResponse.cs            # Ответ после успешной регистрации
│   │   ├── LoginRequest.cs                # Данные для входа пользователя
│   │   ├── LoginResponse.cs               # Ответ после входа: токен, роль, userId
│   │   ├── VerifyTwoFactorRequest.cs      # Данные для проверки 2FA-кода
│   │   └── VerifyTwoFactorResponse.cs     # Ответ после успешного подтверждения 2FA
│   │
│   ├── Common/                            # Общие DTO для разных контроллеров
│   │   └── ApiErrorResponse.cs            # Единый формат ошибки API
│   │
│   ├── Roles/                             # DTO для работы с ролями
│   │   └── RoleResponse.cs                # Ответ с данными роли
│   │
│   └── Users/                             # DTO для работы с пользователями
│       ├── UserResponse.cs                # Ответ с основными данными пользователя
│       ├── UserAccessStatusResponse.cs    # Ответ для проверки доступа пользователя
│       ├── UpdateUserRequest.cs           # Данные для изменения email/phone пользователя
│       ├── UpdateUserRoleRequest.cs       # Данные для изменения роли пользователя
│       └── UpdateUserStatusRequest.cs     # Данные для изменения статуса пользователя
│
├── Enums/                                 # Перечисления сервиса
│   ├── TwoFactorDeliveryChannel.cs        # Канал доставки 2FA-кода: Email или Phone
│   ├── UserRole.cs                        # Роли пользователей: Admin, Client
│   └── UserStatus.cs                      # Статусы пользователей: Registered, Active, Blocked
│
├── Models/                                # Доменные модели сервиса
│   ├── Role.cs                            # Модель роли пользователя
│   └── User.cs                            # Модель пользователя
│
├── Services/                              # Бизнес-логика сервиса
│   ├── AuthService.cs                     # Регистрация, вход и проверка учетных данных
│   ├── JWTTokenService.cs                 # Генерация JWT-токенов
│   ├── PasswordHashService.cs             # Хэширование и проверка паролей
│   └── UserService.cs                     # Получение и изменение данных пользователей
│
├── appsettings.json                       # Конфигурация: БД, JWT, логирование
└── Program.cs                             # Точка входа и настройка приложения
```