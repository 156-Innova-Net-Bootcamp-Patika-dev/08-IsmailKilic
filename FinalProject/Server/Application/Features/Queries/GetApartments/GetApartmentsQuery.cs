using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetApartments
{
    public class GetApartmentsQuery : IRequest<List<GetApartmentsResponse>>
    {
        public string UserId { get; set; }
        public bool ByUser { get; set; }
    }
}
