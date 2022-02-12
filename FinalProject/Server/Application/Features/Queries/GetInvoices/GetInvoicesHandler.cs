using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Queries.GetInvoices
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, List<GetInvoicesResponse>>
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;

        public GetInvoicesHandler(IInvoiceRepository invoiceRepository,IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetInvoicesResponse>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = invoiceRepository.GetList(null, x => x.Apartment);
            return mapper.Map<List<GetInvoicesResponse>>(invoices);
        }
    }
}
