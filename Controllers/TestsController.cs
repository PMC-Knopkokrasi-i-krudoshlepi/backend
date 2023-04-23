using BookStoreApi.Services;
using DPOBackend.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/tests")]
public class TestsController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestById(
        [FromServices] TestService service,
        [FromServices] ImageService imageService,
        [FromRoute]int id)
    {
        var t = await service.GetAsync(id);
        return Ok(new TestToFront(t));//TODO: переделать
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTest(
        [FromServices] TestService service,
        [FromBody] TestRegistrationModel test)
    {
        var t = new TestModel(test);
        await service.CreateAsync(t);
        return Ok(t.Id);
    }
    
    [HttpPost("{id}/data")]
    public async Task<IActionResult> LoadData(
        [FromServices] TestService testService,
        [FromServices] ImageService imageService,
        [FromRoute] int id)
    {
        var imageIds = await imageService.UploadFromStreamAsyncAndGetIds(HttpContext.Request.Form.Files);
        var isSucced = await testService.TryUpdateImageIds(id, imageIds);
        if (isSucced)
            return Ok();
        return NotFound();
    }
}