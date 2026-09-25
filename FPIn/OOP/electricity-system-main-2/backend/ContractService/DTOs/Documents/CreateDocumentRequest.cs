namespace ContractService.DTOs.Documents;

public class CreateDocumentRequest
{
    public Guid UserId { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}