using ContractService.Data;
using ContractService.DTOs.Contracts;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Services;

public class ContractsService
{
    private readonly ContractDbContext _context;

    public ContractsService(ContractDbContext context)
    {
        _context = context;
    }

    public async Task<ContractResponse> CreateContractAsync(CreateContractRequest request)
    {
        if (request.UserId == Guid.Empty)
        {
            throw new InvalidOperationException("Не указан пользователь.");
        }

        if (request.OrganizationId == Guid.Empty)
        {
            throw new InvalidOperationException("Не указана организация.");
        }

        if (string.IsNullOrWhiteSpace(request.ContractNumber))
        {
            throw new InvalidOperationException("Номер договора обязателен.");
        }

        if (string.IsNullOrWhiteSpace(request.TariffType))
        {
            throw new InvalidOperationException("Тип тарифа обязателен.");
        }

        var organization = await _context.Organizations.FirstOrDefaultAsync(x => x.Id == request.OrganizationId);

        if (organization == null)
        {
            throw new InvalidOperationException("Организация не найдена.");
        }

        var existingContract = await _context.Contracts.FirstOrDefaultAsync(x => x.ContractNumber == request.ContractNumber);

        if (existingContract != null)
        {
            throw new InvalidOperationException("Договор с таким номером уже существует.");
        }

        var contract = new Contract
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            OrganizationId = request.OrganizationId,
            ContractNumber = request.ContractNumber,
            Status = "Active",
            TariffType = request.TariffType,
            StartsAt = request.StartsAt,
            EndsAt = request.EndsAt,
            CreatedAt = DateTime.UtcNow
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();

        return new ContractResponse
        {
            ContractId = contract.Id,
            UserId = contract.UserId,
            OrganizationId = contract.OrganizationId,
            ContractNumber = contract.ContractNumber,
            Status = contract.Status,
            TariffType = contract.TariffType,
            StartsAt = contract.StartsAt,
            EndsAt = contract.EndsAt,
            CreatedAt = contract.CreatedAt
        };
    }

    public async Task<ContractResponse?> GetContractByIdAsync(Guid contractId)
    {
        var contract = await _context.Contracts.FirstOrDefaultAsync(x => x.Id == contractId);

        if (contract == null)
        {
            return null;
        }

        return new ContractResponse
        {
            ContractId = contract.Id,
            UserId = contract.UserId,
            OrganizationId = contract.OrganizationId,
            ContractNumber = contract.ContractNumber,
            Status = contract.Status,
            TariffType = contract.TariffType,
            StartsAt = contract.StartsAt,
            EndsAt = contract.EndsAt,
            CreatedAt = contract.CreatedAt
        };
    }

    public async Task<List<ContractResponse>> GetContractsByUserIdAsync(Guid userId)
    {
        var contracts = await _context.Contracts.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();

        var result = new List<ContractResponse>();

        foreach (var contract in contracts)
        {
            result.Add(new ContractResponse
            {
                ContractId = contract.Id,
                UserId = contract.UserId,
                OrganizationId = contract.OrganizationId,
                ContractNumber = contract.ContractNumber,
                Status = contract.Status,
                TariffType = contract.TariffType,
                StartsAt = contract.StartsAt,
                EndsAt = contract.EndsAt,
                CreatedAt = contract.CreatedAt
            });
        }

        return result;
    }

    public async Task<ContractResponse?> UpdateContractStatusAsync(
        Guid contractId,
        UpdateContractStatusRequest request)
    {
        if (request.Status != "Draft" && request.Status != "Active" && request.Status != "Archived")
        {
            throw new InvalidOperationException("Недопустимый статус договора.");
        }

        var contract = await _context.Contracts
            .FirstOrDefaultAsync(x => x.Id == contractId);

        if (contract == null)
        {
            return null;
        }

        contract.Status = request.Status;

        var auditLog = new AuditLog
        {
            Id = Guid.NewGuid(),
            AdminId = request.AdminId,
            Action = "UpdateContractStatus",
            TargetType = "Contract",
            TargetId = contract.Id,
            Description = request.ChangeReason,
            CreatedAt = DateTime.UtcNow
        };

        _context.AuditLogs.Add(auditLog);

        await _context.SaveChangesAsync();

        return new ContractResponse
        {
            ContractId = contract.Id,
            UserId = contract.UserId,
            OrganizationId = contract.OrganizationId,
            ContractNumber = contract.ContractNumber,
            Status = contract.Status,
            TariffType = contract.TariffType,
            StartsAt = contract.StartsAt,
            EndsAt = contract.EndsAt,
            CreatedAt = contract.CreatedAt
        };
    }
}