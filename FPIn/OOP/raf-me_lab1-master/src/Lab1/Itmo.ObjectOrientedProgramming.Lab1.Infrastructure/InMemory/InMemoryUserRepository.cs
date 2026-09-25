using Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.InMemory;

public class InMemoryUserRepository : IUserRepository
{
    public bool Exists(string name) => throw new NotImplementedException();

    public void Add(User user) => throw new NotImplementedException();

    public User? Find(string name) => throw new NotImplementedException();
}
