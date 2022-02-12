using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Commands.Invoices.CreateInvoice
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceRequest, CreateInvoiceResponse>
    {
        public Task<CreateInvoiceResponse> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
