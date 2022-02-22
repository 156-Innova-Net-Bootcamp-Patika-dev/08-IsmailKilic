using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Commands.Admin.Register;
using Application.Features.Commands.Admin.ToggleDelete;
using Application.Features.Queries.GetUsers;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("toggle-delete")]
        public async Task<ToggleDeleteCommandResponse> ToggleUserDeleted(ToggleDeleteCommandRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpGet]
        [Route("users")]
        public async Task<List<GetUsersResponse>> GetAllUsers()
        {
            return await mediator.Send(new GetUsersQuery());
        }
    }
}
