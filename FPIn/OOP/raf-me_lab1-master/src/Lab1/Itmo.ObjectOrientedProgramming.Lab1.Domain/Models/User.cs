using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class User
{
    private readonly HashSet<UserRole> _roles;

    public User(string name, IEnumerable<UserRole> roles)
    {
        Name = name;
        _roles = new HashSet<UserRole>(roles);
    }

    public string Name { get; }

    public IReadOnlyCollection<UserRole> Roles => new ReadOnlyCollection<UserRole>(new List<UserRole>(_roles));

    public bool HasRole(UserRole role) => throw new NotImplementedException();

    public bool HasBorrowed(BookIdentifier identifier) => throw new NotImplementedException();

    public void Borrow(BookIdentifier identifier) => throw new NotImplementedException();

    public void Return(BookIdentifier identifier) => throw new NotImplementedException();
}
