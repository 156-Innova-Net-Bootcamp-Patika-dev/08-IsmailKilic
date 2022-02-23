using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Apartments.UpdateApartment
{
    public class UpdateApartmentCommandRequest : IRequest<UpdateApartmentCommandResponse>
    {
        public int Id { get; set; }
        public char Block { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }
        public OwnerType OwnerType { get; set; }
    }
}
