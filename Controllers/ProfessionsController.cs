using BookStoreApi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/profession")]
public class ProfessionsController: Controller
{
    [HttpGet("getAllProfessions")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetAllProfessions([FromServices] ProfessionService service)
    {
        return Ok(await service.GetAllAsync());
    }
    
    [HttpGet("getAllProfessionsNames")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetAllProfessionsNames([FromServices] ProfessionService service)
    {
        var result = new List<string>();
        foreach (var id in await service.GetAllIdsAsync())    
        {
            result.Add((await service.GetAsync(id))?.Name);
        }
        return Ok(result.ToArray());
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