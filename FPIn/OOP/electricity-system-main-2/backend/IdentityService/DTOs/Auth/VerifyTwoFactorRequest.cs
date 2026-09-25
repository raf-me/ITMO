namespace IdentityService.DTOs.Auth;

public class VerifyTwoFactorRequest
{
    public string TemporaryToken { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}