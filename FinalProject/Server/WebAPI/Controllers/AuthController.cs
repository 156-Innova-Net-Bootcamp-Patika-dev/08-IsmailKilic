using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Commands.Auth.ChangePassword;
using Application.Features.Commands.Auth.Login;
using Application.Features.Commands.Auth.UpdateUser;
using Application.Features.Queries.GetAuthenticatedUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("hello")]
        public IActionResult Hello()
        {
            return Ok("Hello World");
        }

        [HttpPost]
        [Route("login")]
        public async Task<LoginCommandResponse> Login(LoginCommandRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpPut("update-user")]
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            request.Id = userId;
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request)
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            request.Id = userId;
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<GetAuthenticatedUserResponse> GetAuthUser()
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            return await mediator.Send(new GetAuthenticatedUserQuery() { Id = userId });
        }
    }
}
