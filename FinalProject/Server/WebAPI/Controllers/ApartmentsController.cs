using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Features.Commands.Apartments.RemoveUser;
using Application.Features.Queries.GetApartments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ApartmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<CreateApartmentResponse> Register(CreateApartmentRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpGet]
        public async Task<List<GetApartmentsResponse>> GetAll()
        {
            return await mediator.Send(new GetApartmentsQuery());
        }

        [HttpPost]
        [Route("assign-user")]
        public async Task<AssignUserResponse> AssignUser(AssignUserRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpPost]
        [Route("remove-user")]
        public async Task<RemoveUserResponse> RemoveUser(RemoveUserRequest request)
        {
            return await mediator.Send(request);
        }
    }
}
