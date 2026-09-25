namespace ContractService.DTOs.Organizations;

public class CreateOrganizationRequest
{
    public string Name { get; set; } = string.Empty;
    public string Inn { get; set; } = string.Empty;
    public string? LegalAddress { get; set; }
}