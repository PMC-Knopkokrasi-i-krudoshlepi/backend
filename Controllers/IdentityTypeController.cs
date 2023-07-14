using BookStoreApi.Services;
using DPOBackend.Models.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityTypeController: Controller
{
    [HttpGet("getAllIdentities")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetAllIdentities([FromServices] IdentityService service)
    {
        return Ok(await service.GetAllAsync());
    }
    
    [HttpPost("addIdentity")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> AddIdentity([FromServices] IdentityService service, IdentityDTO dto)
    {
        return Ok(await service.CreateAsync(dto));
    }
}