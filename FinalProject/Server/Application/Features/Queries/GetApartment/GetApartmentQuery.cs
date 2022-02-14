using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetApartment
{
    public class GetApartmentQuery : IRequest<GetApartmentResponse>
    {
        public int Id { get; set; }
    }
}
