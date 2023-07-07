using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStoreApi.Services;
using DPOBackend.Models;
using DPOBackend.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using TokenApp;

namespace DPOBackend.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpGet("login")]
    [EnableCors(policyName: "AllowAll")]
    public async Task<IActionResult> Login(
         [FromServices] UserService service,
        [FromQuery] string username,
        [FromQuery] string password)
    {
        var user = Authenticate(service,username, password);
        if (user != null)
        {
            var token = Generate(user);
            return Ok(token);
        }


        return NotFound("User not found");
    }

    private string Generate(UserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };
        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
    
    private UserModel? Authenticate(UserService service,string username, string password)
    {
        var user = service.GetByNameAndPasswordAsync(username, password);
        if (user.Result != null)
        {
            return user.Result;
        }

        return null;
    }
    
    [Obsolete("Use GroupRegistration")]
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisttrationModel? urm, [FromServices] UserService service)
    {
        await service.CreateAsync(urm);
        return Ok();
    }
    
    [HttpPost("registerGroup")]
    public async Task GroupRegistration([FromServices] UserService service)
    {
        var mem = await service.GroupRegistration(HttpContext.Request.Form.Files);
        // byte[] mas = mem.ToArray();
        string file_type = "application/csv";
        string file_name = "codes";
        HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{file_name}.csv\"");
        await HttpContext.Response.Body.WriteAsync(mem.ToArray());
        //return Ok();
        //return File(mem, file_type, file_name);
    }
}