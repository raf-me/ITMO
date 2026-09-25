namespace ContractService.Models;

public class Meter
{
    public Guid Id { get; set; }

    public Guid ContractId { get; set; }

    public Contract? Contract { get; set; }

    public string SerialNumber { get; set; } = string.Empty;

    public string MeterType { get; set; } = string.Empty;

    public string? Location { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}