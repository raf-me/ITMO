using System.ComponentModel.DataAnnotations;
using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs;

public sealed record AssignRoleDto(
    [Required]
    UserRole Role
);
