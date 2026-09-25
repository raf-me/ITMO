namespace ContractService.Models;

public class Contract
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid OrganizationId { get; set; }

    public Organization? Organization { get; set; }

    public string ContractNumber { get; set; } = string.Empty;

    public string Status { get; set; } = "Draft";

    public string TariffType { get; set; } = string.Empty;

    public DateTime StartsAt { get; set; }

    public DateTime? EndsAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Meter> Meters { get; set; } = new();

    public List<Document> Documents { get; set; } = new();
}