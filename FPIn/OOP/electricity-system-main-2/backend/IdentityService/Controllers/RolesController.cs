using IdentityService.Data;
using IdentityService.DTOs.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IdentityDBContext _db;

    public RolesController(IdentityDBContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoleResponse>>> GetRoles()
    {
        var roles = await _db.Roles
            .Select(r => new RoleResponse
            {
                RoleId = r.RoleId,
                Name = r.Name,
                Description = r.Description
            })
            .ToListAsync();

        return Ok(roles);
    }
}
