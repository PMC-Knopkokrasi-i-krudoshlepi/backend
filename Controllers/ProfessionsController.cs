using BookStoreApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/account")]
public class ProfessionsController: Controller
{
    [HttpGet("getAllProfessions")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetAllProfessions([FromServices] ProfessionService service)
    {
        return Ok(await service.GetAllIdsAsync());
    }
    
    [HttpGet("{id}")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetProfessionById(
        [FromServices] ProfessionService service,
        [FromRoute]int id)
    {
        return Ok(await service.GetAsync(id));
    }
    
    [HttpPost("create")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> CreateProfession(
        [FromServices] ProfessionService service,
        [FromBody] Profession p)
    {
        await service.CreateAsync(p);
        return Ok();
    }
}