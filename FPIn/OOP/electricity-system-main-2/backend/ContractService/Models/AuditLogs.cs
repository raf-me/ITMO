namespace ContractService.Models;

public class AuditLog
{
    public Guid Id { get; set; }

    public Guid AdminId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string TargetType { get; set; } = string.Empty;

    public Guid TargetId { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}