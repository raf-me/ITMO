using CarRental.Application.Abstractions.UseCases;
using CarRental.Application.DTOs;
using CarRental.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/cars")]
[Produces("application/json")]
public class CarsController : ControllerBase
{
    private readonly ICarCatalogService _carCatalogService;

    public CarsController(ICarCatalogService carCatalogService)
    {
        _carCatalogService = carCatalogService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Application.Common.PagedResult<CarDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCars(
        [FromQuery] CarStatus? status,
        [FromQuery] CarCategory? category,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(typeof(CarDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddCar(
        [FromBody] CreateCarDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Manager,Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeCarStatus(
        Guid id,
        [FromBody] ChangeCarStatusDto dto,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
