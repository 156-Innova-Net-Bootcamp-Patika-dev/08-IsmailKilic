using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Apartments.AssignUser
{
    public class AssignUserRequest : IRequest<AssignUserResponse>
    {
        public int ApartmentId { get; set; }
        public string UserId { get; set; }
        public OwnerType OwnerType { get; set; }
    }
}
