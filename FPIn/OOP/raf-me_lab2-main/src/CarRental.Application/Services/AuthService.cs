using CarRental.Application.Abstractions.Repositories;
using CarRental.Application.Abstractions.Services;
using CarRental.Application.Abstractions.UseCases;
using CarRental.Application.DTOs;

namespace CarRental.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;

    private readonly IRoleRepository _roleRepo;

    private readonly IPasswordHasher _passwordHasher;

    private readonly IJwtTokenGenerator _jwtGenerator;

    private readonly IUnitOfWork _uow;

    public AuthService(IUserRepository userRepo, IRoleRepository roleRepo,
        IPasswordHasher passwordHasher, IJwtTokenGenerator jwtGenerator, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
        _uow = uow;
    }

    public Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default) =>
        throw new NotImplementedException();
}
