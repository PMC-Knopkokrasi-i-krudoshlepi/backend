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
    public async Task GetResourceById([FromServices] ImageService service, [FromRoute] int id)
    {
        await service.DownloadToStreamAsync(id, HttpContext.Response.Body);
        //return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> PostResource([FromServices] ImageService service)
    {
        /*foreach (var file in HttpContext.Request.Form.Files)
        {
            await using var stream = file.OpenReadStream();
            var t = await service.UploadFromStreamAsync(file.FileName, stream);
        }*/

        var t = await service.UploadFromStreamAsyncAndGetIds(HttpContext.Request.Form.Files);

        return Ok(t);
    }
}