using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using System;

namespace Application.Features.Commands.Invoices.CreateInvoice
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceRequest, CreateInvoiceResponse>
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;
        private readonly IAparmentRepository aparmentRepository;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository,IMapper mapper,
            IAparmentRepository aparmentRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
            this.aparmentRepository = aparmentRepository;
        }

        public async Task<CreateInvoiceResponse> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            var apartment = aparmentRepository.Get(x=>x.Id == request.ApartmentId);
            if (apartment == null) throw new Exception("Daire bulunamadı");

            var invoice = mapper.Map<Invoice>(request);
            invoice.Apartment = apartment;
            invoice.IsPaid = false;

            var newInvoice = await invoiceRepository.Add(invoice);
            return mapper.Map<CreateInvoiceResponse>(newInvoice);
        }
    }
}
