using BookStoreApi.Services;
using DPOBackend.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpGet("{id}")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> Login([FromServices] UserService service)
    {
        var t = await service.GetAsync(id);
        return Ok(new TestToFront(t));//TODO: переделать
    }

    [HttpPost("register")]
    public async Task<IActionResult> GetTestResult()
    {
    }
}