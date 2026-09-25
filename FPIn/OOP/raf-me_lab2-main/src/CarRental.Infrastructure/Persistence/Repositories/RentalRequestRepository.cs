using CarRental.Application.Abstractions.Repositories;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Infrastructure.Persistence.Repositories;

public class RentalRequestRepository : IRentalRequestRepository
{
    private readonly ApplicationDbContext _context;

    public RentalRequestRepository(ApplicationDbContext context) => _context = context;

    public Task<RentalRequest?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<(IReadOnlyList<RentalRequest> Items, int TotalCount)> GetPagedAsync(
        Guid? userId, RentalRequestStatus? status,
        int page, int pageSize, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<bool> HasOverlappingRequestsAsync(
        Guid carId, DateOnly startDate, DateOnly endDate,
        Guid? excludeRequestId = null, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public void Add(RentalRequest request) => throw new NotImplementedException();

    public void Update(RentalRequest request) => throw new NotImplementedException();
}
