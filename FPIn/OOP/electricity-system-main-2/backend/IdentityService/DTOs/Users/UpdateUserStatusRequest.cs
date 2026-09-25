namespace IdentityService.DTOs.Users;

public class UpdateUserStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public Guid AdminId { get; set; }
    public string ChangeReason { get; set; } = string.Empty;
}