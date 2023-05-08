using BookStoreApi.Services;
using DPOBackend.Models;
using DPOBackend.Models.UserModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpGet("login")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> Login(
        [FromServices] UserService service,
        [FromQuery] string name,
        [FromQuery] string password)
    {
        throw new Exception();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel user, [FromServices] UserService service)
    {
        var response = service.CreateAsync(user);
        return Ok(response);
    }
}

