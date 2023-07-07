using BookStoreApi.Services;
using DPOBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/tests")]
public class TestsController : ControllerBase
{
    [HttpGet("{id}")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetTestById(
        [FromServices] TestService service,
        [FromRoute]int id)
    {
        var t = await service.GetAsync(id);
        return Ok(new TestToFront(t));//TODO: переделать
    }

    [HttpPost("result/{id}")]
    public async Task<IActionResult> GetTestResult(
        [FromRoute] int id,
        [FromBody] string[][] answers, 
        [FromServices] TestService service)
    {
        var t = await service.GetTestResult(id, answers);
        return Ok(new
        {
            rightAnswers = t.Item1,
            questionCount = t.Item2
        });
    }

    [HttpGet("all")]
    public async Task<int[]> GetAllTestsIds([FromServices] TestService service)
    {
        return await service.GetAsyncAll();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTest(
        [FromServices] TestService service,
        [FromBody] TestRegistrationModel test)
    {
        var t = new TestModel(service,test);
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