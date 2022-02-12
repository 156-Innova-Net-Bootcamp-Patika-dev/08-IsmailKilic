using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Invoices.CreateInvoice
{
    public class CreateInvoiceRequest : IRequest<CreateInvoiceResponse>
    {
        public InvoiceType InvoiceType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Price { get; set; }
        public int ApartmentId { get; set; }
    }
}
