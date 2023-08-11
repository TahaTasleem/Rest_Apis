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
                var jwtToken = context.Request.Cookies["Token"];

                if (!string.IsNullOrEmpty(jwtToken))
                {
                    Console.WriteLine(jwtToken);
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
