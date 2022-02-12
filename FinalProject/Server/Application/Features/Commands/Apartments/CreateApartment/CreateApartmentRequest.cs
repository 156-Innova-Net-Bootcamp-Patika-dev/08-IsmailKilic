using MediatR;

namespace Application.Features.Commands.Apartments.CreateApartment
{
    public class CreateApartmentRequest : IRequest<CreateApartmentResponse>
    {
        public char Block { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }
    }
}
