namespace IdentityService.DTOs.Auth;

public class EnableTwoFactorConfirmRequest
{
    public string Code { get; set; } = string.Empty;
}
