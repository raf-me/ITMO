namespace ContractService.Models;

public class Document
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? ContractId { get; set; }

    public Contract? Contract { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public string VerificationStatus { get; set; } = "Pending";

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}