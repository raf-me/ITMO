namespace ContractService.DTOs.Documents;

public class DocumentResponse
{
    public Guid DocumentId { get; set; }
    public Guid UserId { get; set; }
    public Guid? ContractId { get; set; }
    public string ContractNumber { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string VerificationStatus { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }
}