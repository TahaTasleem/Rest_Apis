using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace RestApis.JWT
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Get the JWT token from the cookie
                var jwtToken = context.Request.Cookies["Token"];

                if (!string.IsNullOrEmpty(jwtToken))
                {
                    Console.WriteLine(jwtToken);
                    // Remove angle brackets around the token when adding to headers
                    context.Request.Headers.Add("Authorization", $"bearer {jwtToken}");
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
