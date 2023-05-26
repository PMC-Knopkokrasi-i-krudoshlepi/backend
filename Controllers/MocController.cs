using System.Security.Claims;
using BookStoreApi.Services;
using DPOBackend.Models;
using DPOBackend.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/moc")]
public class MocController : ControllerBase
{
    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> Current([FromServices] UserService service)
    {
        var currentUser = GetCurrentUser(service);
        return Ok(currentUser.Name);
    }

    private UserModel? GetCurrentUser(UserService service)
    {
        var identity = HttpContext.User.Identities as ClaimsIdentity;
        if (identity != null)
        {
            var claims = identity.Claims;
            return service.GetAsync(int.Parse(claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value))
                .Result;
        }

        return null;
    }
    
}