using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using System;
using Application.Exceptions;
using MassTransit;
using MessageContracts.Events;

namespace Application.Features.Commands.Invoices.CreateManyInvoices
{
    public class CreateManyInvoicesHandler : IRequestHandler<CreateManyInvoicesRequest, CreateManyInvoicesResponse>
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;
        private readonly IAparmentRepository aparmentRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public CreateManyInvoicesHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
            IAparmentRepository aparmentRepository, IPublishEndpoint publishEndpoint)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
            this.aparmentRepository = aparmentRepository;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task<CreateManyInvoicesResponse> Handle(CreateManyInvoicesRequest request, CancellationToken cancellationToken)
        {
            foreach (var aptId in request.ApartmentIds)
            {
                // Check if invoice already created
                var existedInvoice = invoiceRepository.Get(x => x.InvoiceType == request.InvoiceType &&
                    x.Month == request.Month && x.Year == request.Year &&
                    x.Apartment.Id == aptId);

                if (existedInvoice != null) continue;

                // Check if apartment existed
                var apartment = aparmentRepository.Get(x => x.Id == aptId);
                if (apartment == null) continue;

                var invoice = mapper.Map<Invoice>(request);
                invoice.Apartment = apartment;
                invoice.IsPaid = false;

                var newInvoice = await invoiceRepository.Add(invoice);

                // publish an event
                _ = publishEndpoint.Publish(new InvoiceCreated
                {
                    InvoiceId = newInvoice.Id,
                    ApartmentId = newInvoice.Apartment.Id,
                    Month = newInvoice.Month,
                    Year = newInvoice.Year,
                    InvoiceType = (int)newInvoice.InvoiceType,
                });
            }

            return new CreateManyInvoicesResponse
            {
                Message = "Fatura eklemeleri yapıldı."
            };
        }
    }
}
