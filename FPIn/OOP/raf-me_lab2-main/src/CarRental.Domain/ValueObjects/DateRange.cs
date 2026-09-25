namespace CarRental.Domain.ValueObjects;

public sealed class DateRange : IEquatable<DateRange>
{
    public DateOnly StartDate { get; }

    public DateOnly EndDate { get; }

    public int DurationInDays => throw new NotImplementedException();

    private DateRange(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public static DateRange Create(DateOnly startDate, DateOnly endDate) =>
        throw new NotImplementedException();

    public bool Overlaps(DateRange other) => throw new NotImplementedException();

    public bool Equals(DateRange? other) => throw new NotImplementedException();

    public override bool Equals(object? obj) => throw new NotImplementedException();

    public override int GetHashCode() => throw new NotImplementedException();
}
