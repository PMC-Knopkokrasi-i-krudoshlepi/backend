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
        var token = new JwtSecurityToken(AuthOptions.ISSUER, AuthOptions.AUDIENCE, claims,
            expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
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

    

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel? user, [FromServices] UserService service)
    {
        user.Id = (int) service.GetLenthAsync().Result;
        await service.CreateAsync(user); //TODO: TryCreate()
        return Ok();
    }
}