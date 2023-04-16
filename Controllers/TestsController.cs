using BookStoreApi.Services;
using DPOBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/tests")]
public class TestsController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById([FromServices] TestService service,[FromRoute]int id)
    {
        var t = await service.GetAsync(id);
        return Ok(t);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTest([FromServices] TestService service, [FromBody] TestModel test)
    {
        await service.CreateAsync(test);
        return Ok();
    }
}