using CarRental.Application.Services;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace CarRental.UnitTests.Application;

public class RentalPricingServiceTests
{
    private const string ValidVin = "1HGCM82633A004352";

    private readonly RentalPricingService _service = new();

    private static Car CreateCar(decimal dailyRate)
        => new(Guid.NewGuid(), ValidVin, "Toyota", "Camry", 2020, CarCategory.Economy, dailyRate, 0);

    [Fact]
    public void CalculateBasePrice_ShouldMultiplyDailyRateByDays()
    {
        var car = CreateCar(100m);
        var start = new DateOnly(2026, 5, 1);
        var end   = new DateOnly(2026, 5, 8);

        var result = _service.CalculateBasePrice(car, start, end);

        result.Should().Be(700m);
    }

    [Theory]
    [InlineData(100, 3, 300)]
    [InlineData(200, 5, 1000)]
    public void CalculateBasePrice_ShouldReturnCorrectAmount(decimal dailyRate, int days, decimal expected)
    {
        var car   = CreateCar(dailyRate);
        var start = new DateOnly(2026, 5, 1);
        var end   = start.AddDays(days);

        var result = _service.CalculateBasePrice(car, start, end);

        result.Should().Be(expected);
    }

    [Fact]
    public void CalculateLateFee_ShouldBeZero_WhenReturnedOnTime()
    {
        var car = CreateCar(100m);
        var planned = new DateOnly(2026, 5, 10);

        var fee = _service.CalculateLateFee(car, planned, planned);

        fee.Should().Be(0m);
    }

    [Fact]
    public void CalculateLateFee_ShouldBeZero_WhenReturnedEarly()
    {
        var car = CreateCar(100m);
        var planned = new DateOnly(2026, 5, 10);
        var actual  = new DateOnly(2026, 5, 8);

        var fee = _service.CalculateLateFee(car, planned, actual);

        fee.Should().Be(0m);
    }

    [Fact]
    public void CalculateLateFee_ShouldBePositive_WhenReturnedLate()
    {
        var car = CreateCar(100m);
        var planned = new DateOnly(2026, 5, 10);
        var actual  = new DateOnly(2026, 5, 12);

        var fee = _service.CalculateLateFee(car, planned, actual);

        fee.Should().Be(300m);
    }

    [Theory]
    [InlineData(100, 1, 150)]
    [InlineData(100, 3, 450)]
    [InlineData(200, 2, 600)]
    public void CalculateLateFee_ShouldApplyMultiplier(decimal dailyRate, int lateDays, decimal expected)
    {
        var car     = CreateCar(dailyRate);
        var planned = new DateOnly(2026, 5, 10);
        var actual  = planned.AddDays(lateDays);

        var fee = _service.CalculateLateFee(car, planned, actual);

        fee.Should().Be(expected);
    }

    [Fact]
    public void CalculateTotalPrice_ShouldSumAllComponents()
    {
        var total = _service.CalculateTotalPrice(700m, 300m, 200m);

        total.Should().Be(1200m);
    }

    [Fact]
    public void CalculateTotalPrice_ShouldEqualBasePrice_WhenNoFees()
    {
        var total = _service.CalculateTotalPrice(500m, 0m, 0m);

        total.Should().Be(500m);
    }
}
