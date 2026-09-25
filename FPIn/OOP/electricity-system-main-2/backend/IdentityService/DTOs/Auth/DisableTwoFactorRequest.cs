namespace IdentityService.DTOs.Auth;

public class DisableTwoFactorRequest
{
    public string Code { get; set; } = string.Empty;
}
