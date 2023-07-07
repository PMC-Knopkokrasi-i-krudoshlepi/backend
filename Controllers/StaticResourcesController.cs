using System.Reflection.Metadata;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/resource")]
public class StaticResourcesController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResourceById([FromServices] ImageService service, [FromRoute] int id)
    {
        await service.DownloadToStreamAsync(id, HttpContext.Response.Body);
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostResource([FromServices] ImageService service)
    {
        return Ok(await service.UploadFromStreamAsyncAndGetIds(HttpContext.Request.Form.Files));
    }
}