namespace PaymentAPI.Models.Dtos
{
    public class CreatePaymentDto
    {
        public int InvoiceId { get; set; }
        public int ApartmentId { get; set; }
        public string Last4Number { get; set; }
        public string UserId { get; set; }
        public double Price { get; set; }
    }
}
