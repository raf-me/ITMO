using CarRental.Application.Abstractions.Services;
using CarRental.Application.Abstractions.UseCases;
using CarRental.Application.DTOs;
using CarRental.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/rental-requests")]
[Authorize]
[Produces("application/json")]
public class RentalRequestsController : ControllerBase
{
    private readonly IRentalRequestService _rentalRequestService;

    private readonly ICurrentUserService _currentUserService;

    public RentalRequestsController(
        IRentalRequestService rentalRequestService,
        ICurrentUserService currentUserService)
    {
        _rentalRequestService = rentalRequestService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    [Authorize(Roles = "Client")]
    [ProducesResponseType(typeof(RentalRequestDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateRequest(
        [FromBody] CreateRentalRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [ProducesResponseType(typeof(Application.Common.PagedResult<RentalRequestDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRequests(
        [FromQuery] RentalRequestStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id:guid}/approve")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(RentalContractDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveRequest(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id:guid}/reject")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RejectRequest(
        Guid id,
        [FromBody] RejectRentalRequestDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id:guid}/complete")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(RentalContractDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteRental(
        Guid id,
        [FromBody] CompleteRentalDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
