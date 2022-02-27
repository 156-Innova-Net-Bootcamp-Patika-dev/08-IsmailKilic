using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Features.Commands.Apartments.DeleteApartment;
using Application.Features.Commands.Apartments.RemoveUser;
using Application.Features.Commands.Apartments.UpdateApartment;
using Application.Features.Queries.GetApartment;
using Application.Features.Queries.GetApartments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ApartmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<CreateApartmentResponse> CreateApartment(CreateApartmentRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<DeleteApartmentCommandResponse> DeleteApartment(int id)
        {
            return await mediator.Send(new DeleteApartmentCommandRequest { ApartmentId = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<UpdateApartmentCommandResponse> UpdateApartment(int id, [FromBody] UpdateApartmentCommandRequest request)
        {
            request.Id = id;
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpGet]
        public async Task<List<GetApartmentsResponse>> GetAllApartments([FromQuery] int byUser)
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            return await mediator.Send(new GetApartmentsQuery() { UserId = userId, ByUser = byUser == 1 ? true : false });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<GetApartmentResponse> GetApartmentById(int id)
        {
            return await mediator.Send(new GetApartmentQuery() { Id = id });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("assign-user")]
        public async Task<AssignUserResponse> AssignUserToApartment(AssignUserRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("remove-user")]
        public async Task<RemoveUserResponse> RemoveUserFromApartment(RemoveUserRequest request)
        {
            return await mediator.Send(request);
        }
    }
}
