using MediatR;

namespace Application.Features.Commands.Apartments.RemoveUser
{
    public class RemoveUserRequest : IRequest<RemoveUserResponse>
    {
        public int ApartmentId { get; set; }
    }
}
