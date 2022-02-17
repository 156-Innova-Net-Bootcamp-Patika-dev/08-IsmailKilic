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

namespace Application.Features.Commands.Invoices.CreateInvoice
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceRequest, CreateInvoiceResponse>
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;
        private readonly IAparmentRepository aparmentRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository, IMapper mapper,
            IAparmentRepository aparmentRepository, IPublishEndpoint publishEndpoint)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
            this.aparmentRepository = aparmentRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<CreateInvoiceResponse> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
        {
            // Check if invoice already created
            var existedInvoice = invoiceRepository.Get(x => x.InvoiceType == request.InvoiceType &&
                x.Month == request.Month && x.Year == request.Year &&
                x.Apartment.Id == request.ApartmentId);
            if (existedInvoice != null) throw new BadRequestException("Bu fatura daha önce eklenmiş");

            // Check if apartment existed
            var apartment = aparmentRepository.Get(x => x.Id == request.ApartmentId);
            if (apartment == null) throw new BadRequestException("Daire bulunamadı");

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
            return mapper.Map<CreateInvoiceResponse>(newInvoice);
        }
    }
}
