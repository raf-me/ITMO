using CarRental.Application.DTOs;
using CarRental.Domain.Entities;

namespace CarRental.Application.Mappings;

public static class EntityMappingExtensions
{
    public static CarDto ToDto(this Car car) => throw new NotImplementedException();

    public static UserDto ToDto(this User user) => throw new NotImplementedException();

    public static RentalRequestDto ToDto(this RentalRequest r) => throw new NotImplementedException();

    public static RentalContractDto ToDto(this RentalContract c) => throw new NotImplementedException();
}
