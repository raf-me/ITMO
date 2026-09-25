using CarRental.Application.Abstractions.Repositories;
using CarRental.Application.Abstractions.UseCases;
using CarRental.Application.Common;
using CarRental.Application.DTOs;
using CarRental.Domain.Enums;

namespace CarRental.Application.Services;

public class RentalRequestService : IRentalRequestService
{
    private readonly IRentalRequestRepository _requestRepo;

    private readonly IRentalContractRepository _contractRepo;

    private readonly ICarRepository _carRepo;

    private readonly IUserRepository _userRepo;

    private readonly IUnitOfWork _uow;

    private readonly IRentalEligibilityService _eligibility;

    private readonly IRentalPricingService _pricing;

    public RentalRequestService(
        IRentalRequestRepository requestRepo,
        IRentalContractRepository contractRepo,
        ICarRepository carRepo,
        IUserRepository userRepo,
        IUnitOfWork uow,
        IRentalEligibilityService eligibility,
        IRentalPricingService pricing)
    {
        _requestRepo = requestRepo;
        _contractRepo = contractRepo;
        _carRepo = carRepo;
        _userRepo = userRepo;
        _uow = uow;
        _eligibility = eligibility;
        _pricing = pricing;
    }

    public Task<RentalRequestDto> CreateRequestAsync(
        CreateRentalRequestDto dto, Guid clientId, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<PagedResult<RentalRequestDto>> GetRequestsAsync(
        Guid currentUserId, bool isManager,
        RentalRequestStatus? status, int page, int pageSize,
        CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<RentalContractDto> ApproveRequestAsync(Guid requestId, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task RejectRequestAsync(Guid requestId, string reason, CancellationToken ct = default) =>
        throw new NotImplementedException();

    public Task<RentalContractDto> CompleteRentalAsync(
        Guid requestId, CompleteRentalDto dto, CancellationToken ct = default) =>
        throw new NotImplementedException();
}
