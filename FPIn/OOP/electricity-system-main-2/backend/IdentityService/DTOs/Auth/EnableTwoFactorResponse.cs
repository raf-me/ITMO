namespace IdentityService.DTOs.Auth;

public class EnableTwoFactorResponse
{
    public string SecretKey { get; set; } = string.Empty;
    public string QrCodeBase64 { get; set; } = string.Empty;
    public string ManualEntryKey { get; set; } = string.Empty;
}
