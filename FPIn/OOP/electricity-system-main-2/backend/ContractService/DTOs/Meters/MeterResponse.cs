namespace ContractService.DTOs.Meters;

public class MeterResponse
{
    public Guid MeterId { get; set; }
    public Guid ContractId { get; set; }
    public string SerialNumber { get; set; } = string.Empty;
    public string MeterType { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string Status { get; set; } = string.Empty;
}