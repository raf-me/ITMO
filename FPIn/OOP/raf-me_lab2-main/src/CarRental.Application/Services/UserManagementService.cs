using CarRental.Application.Abstractions.Repositories;
using CarRental.Application.Abstractions.Services;
using CarRental.Application.Abstractions.UseCases;
using CarRental.Application.Common;
using CarRental.Application.DTOs;

namespace CarRental.Application.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _userRepo;

    private readonly IRoleRepository _roleRepo;

    private readonly IPasswordHasher _passwordHasher;

    private readonly IUnitOfWork _uow;

    public UserManagementService(IUserRepository userRepo, IRoleRepository roleRepo,
        IPasswordHasher passwordHasher, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _passwordHasher = passwordHasher;
        _uow = uow;
    }

    public Task<PagedResult<UserDto>> GetUsersAsync(int page, int pageSize, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<UserDto> CreateUserAsync(CreateUserDto dto, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task AssignRoleAsync(Guid userId, AssignRoleDto dto, CancellationToken ct = default) =>
        throw new NotImplementedException();
}
