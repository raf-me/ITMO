namespace Itmo.ObjectOrientedProgramming.Lab1.Models;

public class Book
{
    public Book(BookIdentifier identifier, int editionSize, int initialCopies)
    {
        Identifier = identifier;
        EditionSize = editionSize;
        StoredCopies = initialCopies;
        AvailableCopies = initialCopies;
    }

    public BookIdentifier Identifier { get; }

    public int EditionSize { get; }

    public int StoredCopies { get; private set; }

    public int AvailableCopies { get; private set; }

    public void AddCopies(int amount) => throw new NotImplementedException();

    public void BorrowCopy() => throw new NotImplementedException();

    public void ReturnCopy() => throw new NotImplementedException();
}
