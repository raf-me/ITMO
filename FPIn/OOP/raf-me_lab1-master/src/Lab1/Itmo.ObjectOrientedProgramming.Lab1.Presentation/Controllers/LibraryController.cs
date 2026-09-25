using Itmo.ObjectOrientedProgramming.Lab1.Application.Abstractions;
using Itmo.ObjectOrientedProgramming.Lab1.Models;

namespace Itmo.ObjectOrientedProgramming.Lab1.Presentation.Controllers;

public class LibraryController : ILibraryController
{
    public LibraryController(ILibraryService service)
    {
    }

    public User RegisterUser(string name, IEnumerable<UserRole> roles) => throw new NotImplementedException();

    public LibraryRequest SubmitAddBook(string writerName, string title, string authorName, int editionSize, int quantity) => throw new NotImplementedException();

    public LibraryRequest SubmitBorrowBook(string readerName, string title, string authorName) => throw new NotImplementedException();

    public LibraryRequest SubmitReturnBook(string readerName, string title, string authorName) => throw new NotImplementedException();

    public LibraryRequest Approve(string librarianName, int requestId) => throw new NotImplementedException();

    public LibraryRequest Reject(string librarianName, int requestId) => throw new NotImplementedException();

    public LibraryRequest? GetRequest(int requestId) => throw new NotImplementedException();

    public Book? GetBook(BookIdentifier identifier) => throw new NotImplementedException();
}
