using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CarRental.UnitTests.Domain;

public class RentalRequestTests
{
    private static DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    private static RentalRequest CreateRequest(DateOnly? start = null, DateOnly? end = null)
    {
        var startDate = start ?? Today.AddDays(1);
        var endDate   = end   ?? Today.AddDays(8);
        return new RentalRequest(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), startDate, endDate);
    }

    [Fact]
    public void RentalRequest_ShouldBePending_WhenCreated()
    {
        var request = CreateRequest();

        request.Status.Should().Be(RentalRequestStatus.Pending);
    }

    [Fact]
    public void RentalRequest_ShouldSetDates_WhenCreatedWithValidData()
    {
        var start = Today.AddDays(2);
        var end   = Today.AddDays(9);

        var request = CreateRequest(start, end);

        request.StartDate.Should().Be(start);
        request.EndDate.Should().Be(end);
    }

    [Fact]
    public void Approve_ShouldSetApprovedStatus_WhenRequestIsPending()
    {
        var request = CreateRequest();

        request.Approve();

        request.Status.Should().Be(RentalRequestStatus.Approved);
    }

    [Fact]
    public void Approve_ShouldThrow_WhenRequestIsAlreadyApproved()
    {
        var request = CreateRequest();
        request.Approve();

        Action act = () => request.Approve();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Approve_ShouldThrow_WhenRequestIsRejected()
    {
        var request = CreateRequest();
        request.Reject("тест");

        Action act = () => request.Approve();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Reject_ShouldSetRejectedStatusAndComment_WhenRequestIsPending()
    {
        var request = CreateRequest();
        const string comment = "Автомобиль недоступен";

        request.Reject(comment);

        request.Status.Should().Be(RentalRequestStatus.Rejected);
        request.ManagerComment.Should().Be(comment);
    }

    [Fact]
    public void Reject_ShouldThrow_WhenRequestIsAlreadyApproved()
    {
        var request = CreateRequest();
        request.Approve();

        Action act = () => request.Reject("причина");

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Complete_ShouldSetCompletedStatus_WhenRequestIsApproved()
    {
        var request = CreateRequest();
        request.Approve();

        request.Complete();

        request.Status.Should().Be(RentalRequestStatus.Completed);
    }

    [Fact]
    public void Complete_ShouldThrow_WhenRequestIsPending()
    {
        var request = CreateRequest();

        Action act = () => request.Complete();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Complete_ShouldThrow_WhenRequestIsRejected()
    {
        var request = CreateRequest();
        request.Reject("отказ");

        Action act = () => request.Complete();

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void RentalRequest_ShouldThrow_WhenStartDateIsInPast()
    {
        var pastDate = Today.AddDays(-1);

        Action act = () => CreateRequest(pastDate, Today.AddDays(7));

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void RentalRequest_ShouldThrow_WhenEndDateIsBeforeStartDate()
    {
        var start = Today.AddDays(5);
        var end   = Today.AddDays(3);

        Action act = () => CreateRequest(start, end);

        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void RentalRequest_ShouldThrow_WhenEndDateEqualsStartDate()
    {
        var date = Today.AddDays(5);

        Action act = () => CreateRequest(date, date);

        act.Should().Throw<ArgumentException>();
    }
}
