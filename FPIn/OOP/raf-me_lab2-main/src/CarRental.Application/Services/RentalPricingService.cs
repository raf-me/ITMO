using CarRental.Application.Abstractions.UseCases;
using CarRental.Domain.Entities;

namespace CarRental.Application.Services;

public class RentalPricingService : IRentalPricingService
{
    public decimal CalculateBasePrice(Car car, DateOnly startDate, DateOnly endDate) =>
        throw new NotImplementedException();

    public decimal CalculateLateFee(Car car, DateOnly plannedReturnDate, DateOnly actualReturnDate) =>
        throw new NotImplementedException();

    public decimal CalculateTotalPrice(decimal basePrice, decimal lateFee, decimal damageFee) =>
        throw new NotImplementedException();
}
