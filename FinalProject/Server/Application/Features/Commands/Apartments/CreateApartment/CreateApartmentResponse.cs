
using Domain.Entities;

namespace Application.Features.Commands.Apartments.CreateApartment
{
    public class CreateApartmentResponse
    {
        public int Id { get; set; }
        public char Block { get; set; }
        public bool IsFree { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }
        public OwnerType OwnerType { get; set; }
    }
}
