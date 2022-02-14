using Domain.Entities;
using Domain.ViewModels;

namespace Application.Features.Queries.GetInvoicesByUser
{
    public class GetInvoicesResponse
    {
        public int Id { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }

        public ApartmentVM Apartment { get; set; }
    }
}
