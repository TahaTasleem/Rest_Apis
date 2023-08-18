using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestApis.Data;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using RestApis.Models;
using Microsoft.AspNetCore.Authorization;

namespace RestApis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;
        private void SetJwtTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(10) ,
                Domain="localhost",
                Path = "/",
                SameSite=SameSiteMode.None
            };

            Response.Cookies.Append("Token", token, cookieOptions);
        }
        public AuthController(IConfiguration config,AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                var user = await _context.User.FirstOrDefaultAsync(u => u.Username == login.Username);
                if (user == null)
                {
                    Console.WriteLine(user);
                    return Unauthorized("Invalid username or password");
                }

                if (!VerifyPassword(login.Password, user.Password))
                {
                    Console.WriteLine(user);
                    return Unauthorized("Invalid username or password");
                }

                var token = GenerateJwtToken(user.Username);
                SetJwtTokenCookie(token);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30), 
                signingCredentials: creds
            );
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
