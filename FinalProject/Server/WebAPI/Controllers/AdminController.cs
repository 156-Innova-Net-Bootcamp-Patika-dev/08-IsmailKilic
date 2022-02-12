using System.Threading.Tasks;
using Application.Features.Commands.Admin.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;

        public AdminController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("newuser")]
        public async Task<RegisterCommandResponse> Register(RegisterCommandRequest request)
        {
            return await mediator.Send(request);
        }
    }
}
