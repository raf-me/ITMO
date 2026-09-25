namespace IdentityService.DTOs.Users;

public class UpdateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Guid AdminId { get; set; }
    public string ChangeReason { get; set; } = string.Empty;
}