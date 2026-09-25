namespace ContractService.DTOs.Contracts;

public class ContractResponse
{
    public Guid ContractId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string TariffType { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public DateTime CreatedAt { get; set; }
}