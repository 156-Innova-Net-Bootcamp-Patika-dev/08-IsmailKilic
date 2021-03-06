using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Helpers.Jwt;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;

namespace Business.Helpers.Middleware
{
    /// <summary>
    /// Auth middleware runs before request
    /// It checks whether JWT token provided
    /// Decodes jwt token and adds user info to context
    /// </summary>
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                attachUserToContext(context, jwtUtils, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IJwtUtils jwtUtils, string jwtToken)
        {
            try
            {
                var token = jwtUtils.ValidateToken(jwtToken);

                var id = token.Claims.Where(x => x.Type == "id").SingleOrDefault().Value;

                // attach user to context on successful jwt validation
                context.Items["User"] = new AuthUser
                {
                    Id = int.Parse(id),
                };
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
