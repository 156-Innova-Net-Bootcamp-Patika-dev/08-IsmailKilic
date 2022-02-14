using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetInvoicesByUser
{
    public class GetInvoicesQuery : IRequest<List<GetInvoicesResponse>>
    {
        public string UserId { get; set; }
    }
}
