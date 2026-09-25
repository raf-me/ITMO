using ContractService.Data;
using ContractService.DTOs.Organizations;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;


namespace ContractService.Services;

public class OrganizationService
{
    private readonly ContractDbContext _context;

    public OrganizationService(ContractDbContext context)
    {
        _context = context;
    }

    public async Task<OrganizationResponse> CreateOrganizationAsync(CreateOrganizationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new InvalidOperationException("Название организации обязательно для заполнения.");
        }

        if (string.IsNullOrWhiteSpace(request.Inn))
        {
            throw new InvalidOperationException("ИНН организации обязателен для заполнения.");
        }

        var innExists = await _context.Organizations.AnyAsync(x => x.Inn == request.Inn);

        if (innExists)
        {
            throw new InvalidOperationException("Организация с таким ИНН уже существует.");
        }

        var organization = new Organization
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Inn = request.Inn,
            LegalAddress = request.LegalAddress,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Organizations.Add(organization);
        await _context.SaveChangesAsync();

        return new OrganizationResponse
        {
            OrganizationId = organization.Id,
            Name = organization.Name,
            Inn = organization.Inn,
            LegalAddress = organization.LegalAddress,
            CreatedAt = organization.CreatedAt
        };
    }

    public async Task<OrganizationResponse?> GetOrganizationByIdAsync(Guid organizationId)
    {
        var organization = await _context.Organizations.FirstOrDefaultAsync(x => x.Id == organizationId);

        if (organization is null)
        {
            return null;
        }

        return new OrganizationResponse
        {
            OrganizationId = organization.Id,
            Name = organization.Name,
            Inn = organization.Inn,
            LegalAddress = organization.LegalAddress,
            CreatedAt = organization.CreatedAt
        };
    }
}