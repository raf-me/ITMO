using CarRental.Application.Abstractions.Repositories;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context) => _context = context;

    public Task<Role?> GetByRoleTypeAsync(UserRole role, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken ct = default) =>
        throw new NotImplementedException();
}
