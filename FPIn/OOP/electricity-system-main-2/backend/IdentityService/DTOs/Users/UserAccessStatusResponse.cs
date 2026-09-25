namespace IdentityService.DTOs.Users;

public class UserAccessStatusResponse
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}