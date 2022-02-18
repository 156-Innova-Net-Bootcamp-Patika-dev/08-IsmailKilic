using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetAllInvoices
{
    public class GetAllInvoicesQuery : IRequest<List<GetAllInvoicesResponse>>
    {
    }
}
