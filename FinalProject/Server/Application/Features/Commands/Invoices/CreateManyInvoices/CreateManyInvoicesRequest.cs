using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Invoices.CreateManyInvoices
{
    public class CreateManyInvoicesRequest : IRequest<CreateManyInvoicesResponse>
    {
        public InvoiceType InvoiceType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Price { get; set; }
        public List<int> ApartmentIds { get; set; }
    }
}
