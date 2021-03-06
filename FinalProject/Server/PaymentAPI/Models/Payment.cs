using PaymentAPI.Data;

namespace PaymentAPI.Models
{
    [BsonCollection("payments")]
    public class Payment : IEntity
    {
        public int InvoiceId { get; set; }
        public int ApartmentId { get; set; }
        public string Last4Number { get; set; }
        public string UserId { get; set; }
        public double Price { get; set; }
    }
}
