using IdentityService.DTOs.Common;
using IdentityService.DTOs.Users;
using IdentityService.Exceptions;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        try
        {
            var userIdStr = User.FindFirst("sub")?.Value;
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var response = await _userService.GetUserByIdAsync(userId);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid userId)
    {
        if (!IsAdminOrSelf(userId))
            return Forbid();

        try
        {
            var response = await _userService.GetUserByIdAsync(userId);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpGet("{userId:guid}/access-status")]
    [Authorize]
    public async Task<ActionResult<UserAccessStatusResponse>> GetUserAccessStatus(Guid userId)
    {
        try
        {
            var response = await _userService.GetUserAccessStatusAsync(userId);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<List<UserResponse>>> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }

    [HttpPatch("{userId:guid}")]
    public async Task<ActionResult<UserResponse>> UpdateUser(Guid userId, UpdateUserRequest request)
    {
        if (!IsAdminOrSelf(userId))
            return Forbid();

        try
        {
            var response = await _userService.UpdateUserAsync(userId, request);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpPatch("{userId:guid}/role")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<UserResponse>> UpdateUserRole(Guid userId, UpdateUserRoleRequest request)
    {
        try
        {
            var response = await _userService.UpdateUserRoleAsync(userId, request);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpPatch("{userId:guid}/status")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<UserResponse>> UpdateUserStatus(Guid userId, UpdateUserStatusRequest request)
    {
        try
        {
            var response = await _userService.UpdateUserStatusAsync(userId, request);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    private bool IsAdminOrSelf(Guid userId)
    {
        if (User.IsInRole("Admin"))
            return true;

        var sub = User.FindFirst("sub")?.Value;
        return sub != null && sub == userId.ToString();
    }
}
