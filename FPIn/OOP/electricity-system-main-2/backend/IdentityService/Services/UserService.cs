using IdentityService.Data;
using IdentityService.DTOs.Users;
using IdentityService.Enums;
using IdentityService.Exceptions;
using IdentityService.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Services;

public interface IUserService
{
    Task<UserResponse> GetUserByIdAsync(Guid userId);
    Task<UserAccessStatusResponse> GetUserAccessStatusAsync(Guid userId);
    Task<List<UserResponse>> GetUsersAsync();
    Task<UserResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request);
    Task<UserResponse> UpdateUserRoleAsync(Guid userId, UpdateUserRoleRequest request);
    Task<UserResponse> UpdateUserStatusAsync(Guid userId, UpdateUserStatusRequest request);
}

public class UserService : IUserService
{
    private readonly IdentityDBContext _db;

    public UserService(IdentityDBContext db)
    {
        _db = db;
    }

    public async Task<UserResponse> GetUserByIdAsync(Guid userId)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        return ToResponse(user);
    }

    public async Task<UserAccessStatusResponse> GetUserAccessStatusAsync(Guid userId)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        return new UserAccessStatusResponse
        {
            UserId = user.UserId,
            Role = user.Role.Name,
            Status = user.Status.ToString(),
            IsActive = user.Status == UserStatus.Active
        };
    }

    public async Task<List<UserResponse>> GetUsersAsync()
    {
        var users = await _db.Users
            .Include(u => u.Role)
            .ToListAsync();

        return users.Select(ToResponse).ToList();
    }

    public async Task<UserResponse> UpdateUserAsync(Guid userId, UpdateUserRequest request)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email)
        {
            if (await _db.Users.AnyAsync(u => u.Email == request.Email && u.UserId != userId))
                throw new ServiceException("EMAIL_TAKEN", "Email уже зарегистрирован");
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Phone) && request.Phone != user.Phone)
        {
            if (await _db.Users.AnyAsync(u => u.Phone == request.Phone && u.UserId != userId))
                throw new ServiceException("PHONE_TAKEN", "Телефон уже зарегистрирован");
            user.Phone = request.Phone;
        }

        await _db.SaveChangesAsync();
        return ToResponse(user);
    }

    public async Task<UserResponse> UpdateUserRoleAsync(Guid userId, UpdateUserRoleRequest request)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        if (!Enum.TryParse<UserRole>(request.Role, out var roleEnum))
            throw new ServiceException("INVALID_ROLE", $"Неизвестная роль: {request.Role}");

        user.RoleId = (int)roleEnum;
        await _db.SaveChangesAsync();

        await _db.Entry(user).Reference(u => u.Role).LoadAsync();
        return ToResponse(user);
    }

    public async Task<UserResponse> UpdateUserStatusAsync(Guid userId, UpdateUserStatusRequest request)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId)
            ?? throw new ServiceException("USER_NOT_FOUND", "Пользователь не найден", 404);

        if (!Enum.TryParse<UserStatus>(request.Status, out var statusEnum))
            throw new ServiceException("INVALID_STATUS", $"Неизвестный статус: {request.Status}");

        user.Status = statusEnum;
        await _db.SaveChangesAsync();
        return ToResponse(user);
    }

    private static UserResponse ToResponse(User user) => new()
    {
        UserId = user.UserId,
        Login = user.Login,
        Email = user.Email,
        Phone = user.Phone,
        Role = user.Role.Name,
        Status = user.Status.ToString(),
        IsTwoFactorEnabled = user.IsTwoFactorEnabled
    };
}
