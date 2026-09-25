using CarRental.Application.Abstractions.Services;

namespace CarRental.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId => throw new NotImplementedException();

    public string? Username => throw new NotImplementedException();

    public IReadOnlyList<string> Roles => throw new NotImplementedException();

    public bool IsAuthenticated => throw new NotImplementedException();
}
