namespace ContractService.DTOs.Contracts;

public class UpdateContractStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public Guid AdminId { get; set; }
    public string ChangeReason { get; set; } = string.Empty;
}