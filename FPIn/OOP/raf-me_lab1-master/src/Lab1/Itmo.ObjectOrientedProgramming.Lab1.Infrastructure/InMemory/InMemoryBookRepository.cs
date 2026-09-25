using Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.InMemory;

public class InMemoryBookRepository : IBookRepository
{
    public Book? Find(BookIdentifier identifier) => throw new NotImplementedException();

    public void Add(Book book) => throw new NotImplementedException();
}
