using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetApartments
{
    public class GetApartmentsQuery : IRequest<List<GetApartmentsResponse>>
    {
    }
}
