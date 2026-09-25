using IdentityService.DTOs.Auth;
using IdentityService.DTOs.Common;
using IdentityService.Exceptions;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request)
    {
        try
        {
            var response = await _authService.RegisterAsync(request);
            return CreatedAtAction(nameof(Register), response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [HttpPost("verify-2fa")]
    public async Task<ActionResult<VerifyTwoFactorResponse>> VerifyTwoFactor(VerifyTwoFactorRequest request)
    {
        try
        {
            var response = await _authService.VerifyTwoFactorAsync(request);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [Authorize]
    [HttpPost("2fa/enable")]
    public async Task<ActionResult<EnableTwoFactorResponse>> EnableTwoFactor()
    {
        try
        {
            var userId = GetCurrentUserId();
            var response = await _authService.EnableTwoFactorAsync(userId);
            return Ok(response);
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [Authorize]
    [HttpPost("2fa/confirm")]
    public async Task<IActionResult> ConfirmTwoFactor(EnableTwoFactorConfirmRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            await _authService.ConfirmTwoFactorAsync(userId, request);
            return NoContent();
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    [Authorize]
    [HttpPost("2fa/disable")]
    public async Task<IActionResult> DisableTwoFactor(DisableTwoFactorRequest request)
    {
        try
        {
            var userId = GetCurrentUserId();
            await _authService.DisableTwoFactorAsync(userId, request);
            return NoContent();
        }
        catch (ServiceException ex)
        {
            return StatusCode(ex.StatusCode, new ApiErrorResponse { ErrorCode = ex.ErrorCode, Message = ex.Message });
        }
    }

    private Guid GetCurrentUserId()
    {
        var sub = User.FindFirstValue("sub");
        if (!Guid.TryParse(sub, out var userId))
            throw new ServiceException("INVALID_TOKEN", "Невалидный токен", 401);
        return userId;
    }
}
