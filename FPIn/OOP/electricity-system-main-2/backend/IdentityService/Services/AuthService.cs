using IdentityService.Data;
using IdentityService.DTOs.Auth;
using IdentityService.Enums;
using IdentityService.Exceptions;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace IdentityService.Services;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<VerifyTwoFactorResponse> VerifyTwoFactorAsync(VerifyTwoFactorRequest request);
    Task<EnableTwoFactorResponse> EnableTwoFactorAsync(Guid userId);
    Task ConfirmTwoFactorAsync(Guid userId, EnableTwoFactorConfirmRequest request);
    Task DisableTwoFactorAsync(Guid userId, DisableTwoFactorRequest request);
}

public class AuthService : IAuthService
{
    private readonly IdentityDBContext _db;
    private readonly IPasswordHashService _passwordHash;
    private readonly IJWTTokenService _jwtToken;
    private readonly ITwoFactorService _twoFactor;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IdentityDBContext db,
        IPasswordHashService passwordHash,
        IJWTTokenService jwtToken,
        ITwoFactorService twoFactor,
        IMemoryCache cache,
        ILogger<AuthService> logger)
    {
        _db = db;
        _passwordHash = passwordHash;
        _jwtToken = jwtToken;
        _twoFactor = twoFactor;
        _cache = cache;
        _logger = logger;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        if (await _db.Users.AnyAsync(u => u.Login == request.Login))
            throw new ServiceException("LOGIN_TAKEN", "Логин уже занят");

        if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            throw new ServiceException("EMAIL_TAKEN", "Email уже зарегистрирован");

        if (await _db.Users.AnyAsync(u => u.Phone == request.Phone))
            throw new ServiceException("PHONE_TAKEN", "Телефон уже зарегистрирован");

        var user = new User
        {
            UserId = Guid.NewGuid(),
            Login = request.Login,
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = _passwordHash.Hash(request.Password),
            RoleId = (int)UserRole.Client,
            Status = UserStatus.Registered,
            IsTwoFactorEnabled = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return new RegisterResponse
        {
            UserId = user.UserId,
            Login = user.Login,
            Email = user.Email,
            Phone = user.Phone,
            Role = "Client",
            Status = user.Status.ToString(),
            IsTwoFactorEnabled = false
        };
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Login == request.Login);

        if (user == null || !_passwordHash.Verify(request.Password, user.PasswordHash))
            throw new ServiceException("INVALID_CREDENTIALS", "Неверный логин или пароль", 401);

        if (user.Status == UserStatus.Blocked)
            throw new ServiceException("USER_BLOCKED", "Пользователь заблокирован", 403);

        if (user.IsTwoFactorEnabled)
        {
            var tempToken = _jwtToken.GenerateTemporaryToken(user.UserId);

            if (string.IsNullOrEmpty(user.TwoFactorSecret))
            {
                var code = Random.Shared.Next(100000, 999999).ToString();
                _cache.Set($"2fa:{tempToken}", code, TimeSpan.FromMinutes(5));
                // TODO: deliver code via user.TwoFactorDeliveryChannel (Email/SMS not wired yet)
            }

            return new LoginResponse
            {
                AccessToken = tempToken,
                UserId = user.UserId,
                Login = user.Login,
                Role = user.Role.Name,
                RequiresTwoFactor = true
            };
        }

        if (user.Status == UserStatus.Registered)
        {
            user.Status = UserStatus.Active;
            await _db.SaveChangesAsync();
        }

        return new LoginResponse
        {
            AccessToken = _jwtToken.GenerateAccessToken(user),
            UserId = user.UserId,
            Login = user.Login,
            Role = user.Role.Name,
            RequiresTwoFactor = false
        };
    }

    public async Task<VerifyTwoFactorResponse> VerifyTwoFactorAsync(VerifyTwoFactorRequest request)
    {
        var principal = _jwtToken.ValidateTemporaryToken(request.TemporaryToken);
        if (principal == null)
            throw new ServiceException("INVALID_TOKEN", "Недействительный или просроченный токен", 401);

        var userIdStr = principal.FindFirst("sub")?.Value;
        if (!Guid.TryParse(userIdStr, out var userId))
            throw new ServiceException("INVALID_TOKEN", "Недействительный токен", 401);

        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        if (!string.IsNullOrEmpty(user.TwoFactorSecret))
        {
            if (!_twoFactor.VerifyCode(user.TwoFactorSecret, request.Code))
                throw new ServiceException("INVALID_CODE", "Неверный код подтверждения", 401);
        }
        else
        {
            var cacheKey = $"2fa:{request.TemporaryToken}";
            if (!_cache.TryGetValue(cacheKey, out string? expectedCode) || expectedCode != request.Code)
                throw new ServiceException("INVALID_CODE", "Неверный код подтверждения", 401);
            _cache.Remove(cacheKey);
        }

        if (user.Status == UserStatus.Registered)
        {
            user.Status = UserStatus.Active;
            await _db.SaveChangesAsync();
        }

        return new VerifyTwoFactorResponse
        {
            AccessToken = _jwtToken.GenerateAccessToken(user),
            UserId = user.UserId,
            Login = user.Login,
            Role = user.Role.Name,
            RequiresTwoFactor = false
        };
    }

    public async Task<EnableTwoFactorResponse> EnableTwoFactorAsync(Guid userId)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        var secret = _twoFactor.GenerateSecret();
        user.TwoFactorSecret = secret;
        await _db.SaveChangesAsync();

        return new EnableTwoFactorResponse
        {
            SecretKey = secret,
            QrCodeBase64 = _twoFactor.GenerateQrCode(user.Login, secret),
            ManualEntryKey = secret
        };
    }

    public async Task ConfirmTwoFactorAsync(Guid userId, EnableTwoFactorConfirmRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        if (string.IsNullOrEmpty(user.TwoFactorSecret))
            throw new ServiceException("TWO_FACTOR_NOT_INITIATED", "Сначала вызовите /2fa/enable", 400);

        if (!_twoFactor.VerifyCode(user.TwoFactorSecret, request.Code))
            throw new ServiceException("INVALID_CODE", "Неверный TOTP-код", 401);

        user.IsTwoFactorEnabled = true;
        await _db.SaveChangesAsync();
    }

    public async Task DisableTwoFactorAsync(Guid userId, DisableTwoFactorRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        if (!user.IsTwoFactorEnabled || string.IsNullOrEmpty(user.TwoFactorSecret))
            throw new ServiceException("TWO_FACTOR_NOT_ENABLED", "Двухфакторная аутентификация не включена", 400);

        if (!_twoFactor.VerifyCode(user.TwoFactorSecret, request.Code))
            throw new ServiceException("INVALID_CODE", "Неверный TOTP-код", 401);

        user.IsTwoFactorEnabled = false;
        user.TwoFactorSecret = null;
        await _db.SaveChangesAsync();
    }
}
