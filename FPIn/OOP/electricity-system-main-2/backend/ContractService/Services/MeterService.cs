using ContractService.Data;
using ContractService.DTOs.Meters;
using ContractService.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractService.Services;

public class MetersService
{
    private readonly ContractDbContext _context;

    public MetersService(ContractDbContext context)
    {
        _context = context;
    }

    public async Task<MeterResponse> CreateMeterAsync(Guid contractId, CreateMeterRequest request)
    {
        if (contractId == Guid.Empty)
        {
            throw new InvalidOperationException("Не указан договор.");
        }

        if (string.IsNullOrWhiteSpace(request.SerialNumber))
        {
            throw new InvalidOperationException("Серийный номер прибора учета обязателен.");
        }

        if (string.IsNullOrWhiteSpace(request.MeterType))
        {
            throw new InvalidOperationException("Тип прибора учета обязателен.");
        }

        if (request.MeterType != "SingleTariff" && request.MeterType != "TwoTariff")
        {
            throw new InvalidOperationException("Недопустимый тип прибора учета.");
        }

        var contract = await _context.Contracts
            .FirstOrDefaultAsync(x => x.Id == contractId);

        if (contract == null)
        {
            throw new InvalidOperationException("Договор не найден.");
        }

        var meterWithSameSerialNumber = await _context.Meters.FirstOrDefaultAsync(x => x.SerialNumber == request.SerialNumber);

        if (meterWithSameSerialNumber != null)
        {
            throw new InvalidOperationException("Прибор учета с таким серийным номером уже существует.");
        }

        var meter = new Meter
        {
            Id = Guid.NewGuid(),
            ContractId = contractId,
            SerialNumber = request.SerialNumber,
            MeterType = request.MeterType,
            Location = request.Location,
            Status = "Active",
            CreatedAt = DateTime.UtcNow
        };

        _context.Meters.Add(meter);
        await _context.SaveChangesAsync();

        var response = new MeterResponse
        {
            MeterId = meter.Id,
            ContractId = meter.ContractId,
            SerialNumber = meter.SerialNumber,
            MeterType = meter.MeterType,
            Location = meter.Location,
            Status = meter.Status
        };

        return response;
    }

    public async Task<List<MeterResponse>> GetMetersByContractIdAsync(Guid contractId)
    {
        var meters = await _context.Meters.Where(x => x.ContractId == contractId).ToListAsync();

        var result = new List<MeterResponse>();

        foreach (var meter in meters)
        {
            var response = new MeterResponse
            {
                MeterId = meter.Id,
                ContractId = meter.ContractId,
                SerialNumber = meter.SerialNumber,
                MeterType = meter.MeterType,
                Location = meter.Location,
                Status = meter.Status
            };

            result.Add(response);
        }

        return result;
    }

    public async Task<MeterResponse?> GetMeterByIdAsync(Guid meterId)
    {
        var meter = await _context.Meters.FirstOrDefaultAsync(x => x.Id == meterId);

        if (meter == null)
        {
            return null;
        }

        var response = new MeterResponse
        {
            MeterId = meter.Id,
            ContractId = meter.ContractId,
            SerialNumber = meter.SerialNumber,
            MeterType = meter.MeterType,
            Location = meter.Location,
            Status = meter.Status
        };

        return response;
    }

    public async Task<InternalMeterResponse?> GetInternalMeterByIdAsync(Guid meterId)
    {
        var meter = await _context.Meters.FirstOrDefaultAsync(x => x.Id == meterId);

        if (meter == null)
        {
            return null;
        }

        var contract = await _context.Contracts
            .FirstOrDefaultAsync(x => x.Id == meter.ContractId);

        if (contract == null)
        {
            return null;
        }

        var response = new InternalMeterResponse
        {
            MeterId = meter.Id,
            ContractId = meter.ContractId,
            UserId = contract.UserId,
            ContractNumber = contract.ContractNumber,
            MeterType = meter.MeterType,
            Status = meter.Status,
            TariffType = contract.TariffType
        };

        return response;
    }
}