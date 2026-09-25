namespace ContractService.DTOs.Organizations;

public class OrganizationResponse
{
    public Guid OrganizationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;
    public string? LegalAddress { get; set; }
    public DateTime CreatedAt { get; set; }
}