namespace IdentityService.DTOs.Auth;

public class RegisterResponse
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool IsTwoFactorEnabled { get; set; }
}