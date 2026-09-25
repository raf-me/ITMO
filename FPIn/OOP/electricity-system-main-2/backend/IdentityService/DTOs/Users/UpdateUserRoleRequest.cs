namespace IdentityService.DTOs.Users;

public class UpdateUserRoleRequest
{
    public string Role { get; set; } = string.Empty;
    public Guid AdminId { get; set; }
    public string ChangeReason { get; set; } = string.Empty;
}