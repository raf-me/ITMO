namespace ContractService.DTOs.Meters;

public class CreateMeterRequest
{
    public string SerialNumber { get; set; } = string.Empty;
    public string MeterType { get; set; } = string.Empty;
    public string? Location { get; set; }
}