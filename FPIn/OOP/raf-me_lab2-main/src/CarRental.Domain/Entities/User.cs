using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Username { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public DateOnly DateOfBirth { get; private set; }

    public DateOnly LicenseDate { get; private set; }

    public string Email { get; private set; } = string.Empty;

    private readonly List<Role> _roles = new();

    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private readonly List<RentalRequest> _rentalRequests = new();

    public IReadOnlyCollection<RentalRequest> RentalRequests => _rentalRequests.AsReadOnly();

    private User() { }

    public User(Guid id, string username, string passwordHash, string firstName, string lastName,
        DateOnly dateOfBirth, DateOnly licenseDate, string email)
    {
        throw new NotImplementedException();
    }

    public int AgeInYears => throw new NotImplementedException();

    public int DrivingExperienceYears => throw new NotImplementedException();

    public void AddRole(Role role) => throw new NotImplementedException();
}
