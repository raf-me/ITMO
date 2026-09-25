namespace ReadingService.DTOs.Readings;

public class ReadingSubmissionResponse
{
    public Guid SubmissionId { get; set; }
    public Guid UserId { get; set; }
    public Guid ContractId { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public List<MeterReadingResponse> Readings { get; set; } = new();
}