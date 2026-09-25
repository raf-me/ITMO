using CarRental.Application.Abstractions.Repositories;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Persistence.Repositories;

public class RentalContractRepository : IRentalContractRepository
{
    private readonly ApplicationDbContext _context;

    public RentalContractRepository(ApplicationDbContext context) => _context = context;

    public Task<RentalContract?> GetByRentalRequestIdAsync(Guid rentalRequestId, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<RentalContract?> GetByIdWithDetailsAsync(Guid id, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public void Add(RentalContract contract) => throw new NotImplementedException();

    public void Update(RentalContract contract) => throw new NotImplementedException();
}
