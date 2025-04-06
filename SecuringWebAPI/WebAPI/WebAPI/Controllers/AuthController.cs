using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] Credential credential)
        {

            if (credential.Username=="string" && credential.Password=="password")
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "admin"),
                new Claim(ClaimTypes.Email, "admin@example.com"), 
                new Claim("department", "HR") //only the HR can access that.
            };


                //expired at .
                var expiresAt = DateTime.UtcNow.AddMinutes(10);
                return Ok(new
                {
                    access_token = await CreateToken(claims, expiresAt),
                    expiresAt = expiresAt,
                });
            }


                ModelState.AddModelError("Unauthorized", "You are not authorized to access the endpoint");
                return Unauthorized();

            
        }



        private async Task<string> CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey")??""); 
            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt); 
        }
    }




    public class Credential {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
