namespace ReadingService.DTOs.Readings;

public class CreateReadingSubmissionRequest
{
    public Guid UserId { get; set; }
    public Guid ContractId { get; set; }
    public string Period { get; set; } = string.Empty;
    public List<ReadingItemRequest> Readings { get; set; } = new();
}