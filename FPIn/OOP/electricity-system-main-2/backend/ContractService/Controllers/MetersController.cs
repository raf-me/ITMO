using ContractService.DTOs.Common;
using ContractService.DTOs.Meters;
using ContractService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractService.Controllers;

[ApiController]
public class MetersController : ControllerBase
{
    private readonly MetersService _metersService;

    public MetersController(MetersService metersService)
    {
        _metersService = metersService;
    }

    [HttpPost("api/contracts/{contractId:guid}/meters")]
    public async Task<ActionResult<MeterResponse>> CreateMeter(
        Guid contractId,
        CreateMeterRequest request)
    {
        try
        {
            var result = await _metersService.CreateMeterAsync(contractId, request);

            return CreatedAtAction(
                nameof(GetMeterById),
                new { meterId = result.MeterId },
                result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                ErrorCode = "METER_VALIDATION_ERROR",
                Message = ex.Message
            });
        }
    }

    [HttpGet("api/contracts/{contractId:guid}/meters")]
    public async Task<ActionResult<List<MeterResponse>>> GetMetersByContractId(Guid contractId)
    {
        var result = await _metersService.GetMetersByContractIdAsync(contractId);

        return Ok(result);
    }

    [HttpGet("api/meters/{meterId:guid}")]
    public async Task<ActionResult<MeterResponse>> GetMeterById(Guid meterId)
    {
        var result = await _metersService.GetMeterByIdAsync(meterId);

        if (result == null)
        {
            return NotFound(new ApiErrorResponse
            {
                ErrorCode = "METER_NOT_FOUND",
                Message = "Прибор учета не найден."
            });
        }

        return Ok(result);
    }

    [HttpGet("internal/meters/{meterId:guid}")]
    public async Task<ActionResult<InternalMeterResponse>> GetInternalMeterById(Guid meterId)
    {
        var result = await _metersService.GetInternalMeterByIdAsync(meterId);

        if (result == null)
        {
            return NotFound(new ApiErrorResponse
            {
                ErrorCode = "METER_NOT_FOUND",
                Message = "Прибор учета не найден."
            });
        }

        return Ok(result);
    }
}