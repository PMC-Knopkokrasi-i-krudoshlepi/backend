using BookStoreApi.Services;
using DPOBackend.Models.Course;
using DPOBackend.Models.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/cources")]
public class CourseController: Controller
{
    [HttpGet("getAllCources")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> GetAllCources([FromServices] CourseService service)
    {
        return Ok(await service.GetAllAsync());
    }
    
    [HttpPost("addCourse")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> AddCourse([FromServices] CourseService service, CourseDTO dto)
    {
        return Ok(await service.CreateAsync(dto));
    }
}