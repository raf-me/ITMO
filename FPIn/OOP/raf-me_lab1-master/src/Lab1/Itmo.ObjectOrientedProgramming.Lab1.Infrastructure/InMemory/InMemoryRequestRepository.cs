using Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.InMemory;

public class InMemoryRequestRepository : IRequestRepository
{
    public void Add(LibraryRequest request) => throw new NotImplementedException();

    public LibraryRequest? Find(int id) => throw new NotImplementedException();

    public void Remove(int id) => throw new NotImplementedException();
}
