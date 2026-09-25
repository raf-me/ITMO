namespace ContractService.Models;

public class Organization
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Inn { get; set; } = string.Empty;

    public string? Kpp { get; set; }

    public string? Ogrn { get; set; }

    public string? LegalAddress { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Contract> Contracts { get; set; } = new();
}