using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestApis.Data;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace RestApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config,AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);

                if (user == null || !VerifyPassword(password, user.Password))
                {
                    return Unauthorized("Invalid username or password");
                }

                var token = GenerateJwtToken(username);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred");
            }
        }
        private bool VerifyPassword(string password, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHashedPassword);
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, username),
            // Add additional claims as needed
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30), // Set token expiration
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
