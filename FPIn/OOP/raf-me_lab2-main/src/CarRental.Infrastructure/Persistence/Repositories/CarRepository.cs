using CarRental.Application.Abstractions.Repositories;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Infrastructure.Persistence.Repositories;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _context;

    public CarRepository(ApplicationDbContext context) => _context = context;

    public Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<Car?> GetByVinAsync(string vin, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<(IReadOnlyList<Car> Items, int TotalCount)> GetPagedAsync(
        CarStatus? status, CarCategory? category,
        decimal? minPricePerDay, decimal? maxPricePerDay,
        int page, int pageSize, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<bool> ExistsByVinAsync(string vin, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public void Add(Car car) => throw new NotImplementedException();

    public void Update(Car car) => throw new NotImplementedException();
}
