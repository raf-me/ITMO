namespace ReadingService.DTOs.Readings;

public class ReadingItemRequest
{
    public Guid MeterId { get; set; }
    public decimal? DayValue { get; set; }
    public decimal? NightValue { get; set; }
    public string? PhotoUrl { get; set; }
}