namespace ContractService.DTOs.Meters;

public class InternalMeterResponse
{
    public Guid MeterId { get; set; }
    public Guid ContractId { get; set; }
    public Guid UserId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string MeterType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string TariffType { get; set; } = string.Empty;
}