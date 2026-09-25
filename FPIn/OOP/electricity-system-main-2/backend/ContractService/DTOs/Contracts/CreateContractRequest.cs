namespace ContractService.DTOs.Contracts;

public class CreateContractRequest
{
    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string TariffType { get; set; } = string.Empty;
    public DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
}