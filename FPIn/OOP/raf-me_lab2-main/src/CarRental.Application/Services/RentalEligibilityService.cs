using CarRental.Application.Abstractions.UseCases;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Application.Services;

public class RentalEligibilityService : IRentalEligibilityService
{
    public void EnsureEligible(User user, CarCategory category) =>
        throw new NotImplementedException();
}
