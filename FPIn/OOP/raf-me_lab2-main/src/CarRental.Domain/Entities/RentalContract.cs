namespace CarRental.Domain.Entities;

public class RentalContract
{
    public Guid Id { get; private set; }

    public Guid RentalRequestId { get; private set; }

    public RentalRequest RentalRequest { get; private set; } = null!;

    public decimal BasePrice { get; private set; }

    public DateOnly? ActualReturnDate { get; private set; }

    public decimal LateFee { get; private set; }

    public decimal DamageFee { get; private set; }

    public decimal TotalPrice { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private RentalContract() { }

    public RentalContract(Guid id, Guid rentalRequestId, decimal basePrice)
    {
        throw new NotImplementedException();
    }

    public void Complete(DateOnly actualReturnDate, decimal lateFee, decimal damageFee)
    {
        throw new NotImplementedException();
    }
}
