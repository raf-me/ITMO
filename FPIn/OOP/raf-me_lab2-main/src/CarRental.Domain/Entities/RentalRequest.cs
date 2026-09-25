using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

public class RentalRequest
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; } = null!;

    public Guid CarId { get; private set; }

    public Car Car { get; private set; } = null!;

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public RentalRequestStatus Status { get; private set; }

    public string? ManagerComment { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public RentalContract? Contract { get; private set; }

    private RentalRequest() { }

    public RentalRequest(Guid id, Guid userId, Guid carId, DateOnly startDate, DateOnly endDate)
    {
        throw new NotImplementedException();
    }

    public void Approve() => throw new NotImplementedException();

    public void Reject(string comment) => throw new NotImplementedException();

    public void Complete() => throw new NotImplementedException();
}
