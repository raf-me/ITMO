using CarRental.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CarRental.UnitTests.Domain;

public class DateRangeTests
{
    private static DateOnly D(int year, int month, int day) => new(year, month, day);

    [Fact]
    public void Create_ShouldSucceed_WhenEndDateIsAfterStartDate()
    {
        var range = DateRange.Create(D(2026, 5, 1), D(2026, 5, 8));

        range.StartDate.Should().Be(D(2026, 5, 1));
        range.EndDate.Should().Be(D(2026, 5, 8));
    }

    [Fact]
    public void Create_ShouldThrow_WhenEndDateEqualsStartDate()
    {
        Action act = () => DateRange.Create(D(2026, 5, 1), D(2026, 5, 1));

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Create_ShouldThrow_WhenEndDateIsBeforeStartDate()
    {
        Action act = () => DateRange.Create(D(2026, 5, 10), D(2026, 5, 5));

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void DurationInDays_ShouldReturnCorrectValue()
    {
        var range = DateRange.Create(D(2026, 5, 1), D(2026, 5, 8));

        range.DurationInDays.Should().Be(7);
    }

    [Theory]
    [InlineData("2026-05-01", "2026-05-10", "2026-05-05", "2026-05-15", true)]
    [InlineData("2026-05-01", "2026-05-10", "2026-05-10", "2026-05-20", false)]
    [InlineData("2026-05-01", "2026-05-10", "2026-05-11", "2026-05-20", false)]
    [InlineData("2026-05-05", "2026-05-15", "2026-05-01", "2026-05-08", true)]
    [InlineData("2026-05-01", "2026-05-20", "2026-05-05", "2026-05-10", true)]
    public void Overlaps_ShouldReturnCorrectResult(
        string s1, string e1, string s2, string e2, bool expected)
    {
        var r1 = DateRange.Create(DateOnly.Parse(s1), DateOnly.Parse(e1));
        var r2 = DateRange.Create(DateOnly.Parse(s2), DateOnly.Parse(e2));

        r1.Overlaps(r2).Should().Be(expected);
    }

    [Fact]
    public void Equals_ShouldBeTrue_ForSameDates()
    {
        var r1 = DateRange.Create(D(2026, 5, 1), D(2026, 5, 8));
        var r2 = DateRange.Create(D(2026, 5, 1), D(2026, 5, 8));

        r1.Should().Be(r2);
    }

    [Fact]
    public void Equals_ShouldBeFalse_ForDifferentDates()
    {
        var r1 = DateRange.Create(D(2026, 5, 1), D(2026, 5, 8));
        var r2 = DateRange.Create(D(2026, 5, 1), D(2026, 5, 9));

        r1.Should().NotBe(r2);
    }
}
