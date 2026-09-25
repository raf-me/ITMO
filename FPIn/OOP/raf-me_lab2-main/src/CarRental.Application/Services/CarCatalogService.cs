using CarRental.Application.Abstractions.Repositories;
using CarRental.Application.Abstractions.UseCases;
using CarRental.Application.Common;
using CarRental.Application.DTOs;
using CarRental.Domain.Enums;

namespace CarRental.Application.Services;

public class CarCatalogService : ICarCatalogService
{
    private readonly ICarRepository _carRepo;

    private readonly IUnitOfWork _uow;

    public CarCatalogService(ICarRepository carRepo, IUnitOfWork uow)
    {
        _carRepo = carRepo;
        _uow = uow;
    }

    public Task<PagedResult<CarDto>> GetCarsAsync(CarFilterDto filter, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<CarDto?> GetCarByIdAsync(Guid id, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<CarDto> AddCarAsync(CreateCarDto dto, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task ChangeCarStatusAsync(Guid carId, CarStatus newStatus, CancellationToken ct = default) =>
        throw new NotImplementedException();
}
