using OtpNet;
using QRCoder;

namespace IdentityService.Services;

public interface ITwoFactorService
{
    string GenerateSecret();
    string GenerateQrCode(string login, string secret);
    bool VerifyCode(string secret, string code);
}

public class TwoFactorService : ITwoFactorService
{
    public string GenerateSecret()
    {
        var key = KeyGeneration.GenerateRandomKey(20);
        return Base32Encoding.ToString(key);
    }

    public string GenerateQrCode(string login, string secret)
    {
        var otpauthUrl = $"otpauth://totp/IdentityService:{Uri.EscapeDataString(login)}?secret={secret}&issuer=IdentityService";

        using var qrGenerator = new QRCodeGenerator();
        var qrData = qrGenerator.CreateQrCode(otpauthUrl, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrData);
        var pngBytes = qrCode.GetGraphic(20);
        return Convert.ToBase64String(pngBytes);
    }

    public bool VerifyCode(string secret, string code)
    {
        var secretBytes = Base32Encoding.ToBytes(secret);
        var totp = new Totp(secretBytes);
        return totp.VerifyTotp(DateTime.UtcNow, code, out _, new VerificationWindow(1, 1));
    }
}
