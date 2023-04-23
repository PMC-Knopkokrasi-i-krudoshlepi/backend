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
    public async Task<IActionResult> GetResourceById([FromServices] ImageService service, [FromRoute] string id)
    {
        await service.DownloadToStreamAsync(new ObjectId(id), HttpContext.Response.Body);
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostResource([FromServices] ImageService service)
    {
        foreach (var file in HttpContext.Request.Form.Files)
        {
            await using var stream = file.OpenReadStream();
            await service.UploadFromStreamAsync(file.FileName, stream);
        }

        return Ok();
    }
}