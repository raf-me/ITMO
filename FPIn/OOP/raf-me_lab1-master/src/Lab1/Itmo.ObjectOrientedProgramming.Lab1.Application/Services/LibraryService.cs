using Itmo.ObjectOrientedProgramming.Lab1.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab1.Infrastructure.Abstractions.Repositories;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Application.Services;

public class LibraryService : ILibraryService
{
    public LibraryService(IUserRepository userRepository, IBookRepository bookRepository, IRequestRepository requestRepository)
    {
    }

    public User RegisterUser(string name, IEnumerable<UserRole> roles) => throw new NotImplementedException();

    public LibraryRequest CreateAddBookRequest(
        string writerName,
        string title,
        string authorName,
        int editionSize,
        int quantity) => throw new NotImplementedException();

    public LibraryRequest CreateBorrowBookRequest(string readerName, string title, string authorName) => throw new NotImplementedException();

    public LibraryRequest CreateReturnBookRequest(string readerName, string title, string authorName) => throw new NotImplementedException();

    public LibraryRequest ApproveRequest(string librarianName, int requestId) => throw new NotImplementedException();

    public LibraryRequest RejectRequest(string librarianName, int requestId) => throw new NotImplementedException();

    public LibraryRequest? FindRequest(int requestId) => throw new NotImplementedException();

    public Book? FindBook(BookIdentifier identifier) => throw new NotImplementedException();
}
