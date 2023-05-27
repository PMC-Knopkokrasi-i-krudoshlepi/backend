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
        var userId = HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
        var userName = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
        
        return Ok(new {Name = userName,Password = userId});
    }

    
    
}