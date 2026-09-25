using ContractService.DTOs.Common;
using ContractService.DTOs.Organizations;
using ContractService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContractService.Controllers;

[ApiController]
[Route("api/organizations")]
public class OrganizationsController : ControllerBase
{
    private readonly OrganizationService _organizationService;

    public OrganizationsController(OrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpPost]
    public async Task<ActionResult<OrganizationResponse>> CreateOrganization(
        CreateOrganizationRequest request)
    {
        try
        {
            var result = await _organizationService.CreateOrganizationAsync(request);
            return CreatedAtAction(
                nameof(GetOrganizationById),
                new { organizationId = result.OrganizationId },
                result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiErrorResponse
            {
                ErrorCode = "ORGANIZATION_VALIDATION_ERROR",
                Message = ex.Message
            });
        }
    }

    [HttpGet("{organizationId:guid}")]
    public async Task<ActionResult<OrganizationResponse>> GetOrganizationById(Guid organizationId)
    {
        var result = await _organizationService.GetOrganizationByIdAsync(organizationId);

        if (result is null)
        {
            return NotFound(new ApiErrorResponse
            {
                ErrorCode = "ORGANIZATION_NOT_FOUND",
                Message = "Организация не найдена."
            });
        }

        return Ok(result);
    }
}