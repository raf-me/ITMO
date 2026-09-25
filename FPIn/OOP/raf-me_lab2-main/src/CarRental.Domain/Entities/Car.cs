using CarRental.Domain.Enums;
using CarRental.Domain.ValueObjects;

namespace CarRental.Domain.Entities;

public class Car
{
    public Guid Id { get; private set; }

    public Vin Vin { get; private set; } = null!;

    public string Make { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public int Year { get; private set; }

    public CarCategory Category { get; private set; }

    public CarStatus Status { get; private set; }

    public decimal DailyRate { get; private set; }

    public int Mileage { get; private set; }

    private readonly List<RentalRequest> _rentalRequests = new();

    public IReadOnlyCollection<RentalRequest> RentalRequests => _rentalRequests.AsReadOnly();

    private Car() { }

    public Car(Guid id, string vin, string make, string model, int year,
        CarCategory category, decimal dailyRate, int mileage)
    {
        throw new NotImplementedException();
    }

    public void Rent() => throw new NotImplementedException();

    public void Complete() => throw new NotImplementedException();

    public void SendToMaintenance() => throw new NotImplementedException();

    public void ReturnFromMaintenance() => throw new NotImplementedException();
}
