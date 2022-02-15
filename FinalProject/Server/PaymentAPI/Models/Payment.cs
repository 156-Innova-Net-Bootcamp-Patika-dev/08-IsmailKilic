using PaymentAPI.Data;

namespace PaymentAPI.Models
{
    [BsonCollection("payments")]
    public class Payment : IEntity
    {
        public int InvoiceId { get; set; }
    }
}
