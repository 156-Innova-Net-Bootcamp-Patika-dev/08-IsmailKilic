using Domain.Entities;

namespace Domain.ViewModels
{
    public class InvoiceVM
    {
        public int Id { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }
    }
}
