using CarRental.Application.Abstractions.Services;
using CarRental.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CarRental.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user) => throw new NotImplementedException();
}
