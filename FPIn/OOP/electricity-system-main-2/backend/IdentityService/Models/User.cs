using IdentityService.Enums;

namespace IdentityService.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Login { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Registered;
    public bool IsTwoFactorEnabled { get; set; }
    public TwoFactorDeliveryChannel? TwoFactorDeliveryChannel { get; set; }
    public string? TwoFactorSecret { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Role Role { get; set; } = null!;
}
