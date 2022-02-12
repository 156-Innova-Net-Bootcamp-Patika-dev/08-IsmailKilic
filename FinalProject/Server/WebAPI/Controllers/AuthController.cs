using System.Threading.Tasks;
using Application.Features.Commands.Auth.Login;
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

        //[HttpPost]
        //[Route("register")]
        //public async Task<RegisterCommandResponse> Register(RegisterCommandRequest request)
        //{
        //    return await mediator.Send(request);
        //}
    }
}
