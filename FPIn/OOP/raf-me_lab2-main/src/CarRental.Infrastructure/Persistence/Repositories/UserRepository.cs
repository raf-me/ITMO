using CarRental.Application.Abstractions.Repositories;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context) => _context = context;

    public Task<User?> GetByIdWithRolesAsync(Guid id, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<User?> GetByUsernameWithRolesAsync(string username, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<(IReadOnlyList<User> Items, int TotalCount)> GetPagedAsync(
        int page, int pageSize, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public void Add(User user) => throw new NotImplementedException();

    public void Update(User user) => throw new NotImplementedException();
}
