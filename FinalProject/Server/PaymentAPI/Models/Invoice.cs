using PaymentAPI.Data;

namespace PaymentAPI.Models
{
    [BsonCollection("invoices")]
    public class Invoice : IEntity
    {
        public int InvoiceId { get; set; }
        public int ApartmentId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int InvoiceType { get; set; }
    }
}
