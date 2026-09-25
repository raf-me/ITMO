using ContractService.DTOs.Common;
using ContractService.DTOs.Contracts;
using ContractService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractService.Controllers;

[ApiController]
[Route("api/contracts")]
public class ContractsController : ControllerBase
{
    private readonly ContractsService _contractsService;

    public ContractsController(ContractsService contractsService)
    {
        _contractsService = contractsService;
    }

    [HttpPost]
    public async Task<ActionResult<ContractResponse>> CreateContract(CreateContractRequest request)
    {
        try
        {
            var result = await _contractsService.CreateContractAsync(request);

            return CreatedAtAction(
                nameof(GetContractById),
                new { contractId = result.ContractId },
                result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                ErrorCode = "CONTRACT_VALIDATION_ERROR",
                Message = ex.Message
            });
        }
    }

    [HttpGet("{contractId:guid}")]
    public async Task<ActionResult<ContractResponse>> GetContractById(Guid contractId)
    {
        var result = await _contractsService.GetContractByIdAsync(contractId);

        if (result == null)
        {
            return NotFound(new ApiErrorResponse
            {
                ErrorCode = "CONTRACT_NOT_FOUND",
                Message = "Договор не найден."
            });
        }

        return Ok(result);
    }

    [HttpGet("/api/users/{userId:guid}/contracts")]
    public async Task<ActionResult<List<ContractResponse>>> GetContractsByUserId(Guid userId)
    {
        var result = await _contractsService.GetContractsByUserIdAsync(userId);

        return Ok(result);
    }

    [HttpPatch("{contractId:guid}/status")]
    public async Task<ActionResult<ContractResponse>> UpdateContractStatus(
        Guid contractId,
        UpdateContractStatusRequest request)
    {
        try
        {
            var result = await _contractsService.UpdateContractStatusAsync(contractId, request);

            if (result == null)
            {
                return NotFound(new ApiErrorResponse
                {
                    ErrorCode = "CONTRACT_NOT_FOUND",
                    Message = "Договор не найден."
                });
            }

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                ErrorCode = "CONTRACT_STATUS_ERROR",
                Message = ex.Message
            });
        }
    }
}