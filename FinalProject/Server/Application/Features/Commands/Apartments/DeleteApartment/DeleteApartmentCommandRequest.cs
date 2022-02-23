using MediatR;

namespace Application.Features.Commands.Apartments.DeleteApartment
{
    public class DeleteApartmentCommandRequest : IRequest<DeleteApartmentCommandResponse>
    {
        public int ApartmentId { get; set; }
    }
}
