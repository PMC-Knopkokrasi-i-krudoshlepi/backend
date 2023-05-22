using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BookStoreApi.Services;
using DPOBackend.Models;
using DPOBackend.Models.UserModels;
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
        var identity = await GetIdentity(service, username, password);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid username or password." });
        }
 
        var now = DateTime.UtcNow;
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };
 
        return Ok(response);
    }
    
    private async Task<ClaimsIdentity?> GetIdentity(UserService service,string username, string password) =>
        await Task.Run(async () =>
        {
            var user = await service.GetByNameAndPassword(username, password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        });
    

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserModel user, [FromServices] UserService service)
    {
        await service.CreateAsync(user); //TODO: TryCreate()
        return Ok();
    }
}

