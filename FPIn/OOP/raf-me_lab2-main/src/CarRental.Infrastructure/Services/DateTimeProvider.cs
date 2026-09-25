using CarRental.Application.Abstractions.Services;

namespace CarRental.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => throw new NotImplementedException();

    public DateOnly UtcToday => throw new NotImplementedException();
}
