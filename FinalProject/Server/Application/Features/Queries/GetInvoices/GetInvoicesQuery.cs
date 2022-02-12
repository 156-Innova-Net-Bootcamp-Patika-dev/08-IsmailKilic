using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetInvoices
{
    public class GetInvoicesQuery : IRequest<List<GetInvoicesResponse>>
    {
    }
}
