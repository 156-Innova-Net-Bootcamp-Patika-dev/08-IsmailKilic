using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Queries.GetAllInvoices
{
    public class GetAllInvoicesHandler : IRequestHandler<GetAllInvoicesQuery, List<GetAllInvoicesResponse>>
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;

        public GetAllInvoicesHandler(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetAllInvoicesResponse>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = invoiceRepository.GetList(null, x => x.Apartment);
            return mapper.Map<List<GetAllInvoicesResponse>>(invoices);
        }
    }
}
