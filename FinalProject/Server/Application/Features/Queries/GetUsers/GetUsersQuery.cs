using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<List<GetUsersResponse>>
    {
    }
}
