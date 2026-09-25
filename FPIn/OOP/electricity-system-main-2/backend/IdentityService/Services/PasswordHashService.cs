namespace IdentityService.Services;

public interface IPasswordHashService
{
    string Hash(string password);
    bool Verify(string password, string hash);
}

public class PasswordHashService : IPasswordHashService
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}
