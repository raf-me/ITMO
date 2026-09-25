namespace ReadingService.DTOs.Readings;

public class UpdateReadingSubmissionStatusRequest
{
    public string Status { get; set; } = string.Empty;
    public Guid AdminId { get; set; }
    public string? Comment { get; set; }
}