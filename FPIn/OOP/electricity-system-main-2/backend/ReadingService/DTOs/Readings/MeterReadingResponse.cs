namespace ReadingService.DTOs.Readings;

public class MeterReadingResponse
{
    public Guid MeterReadingId { get; set; }
    public Guid MeterId { get; set; }
    public decimal? DayValue { get; set; }
    public decimal? NightValue { get; set; }
    public string? PhotoUrl { get; set; }
}