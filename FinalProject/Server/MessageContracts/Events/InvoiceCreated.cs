namespace MessageContracts.Events
{
    public class InvoiceCreated
    {
        public int InvoiceId { get; set; }
        public int ApartmentId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int InvoiceType { get; set; }
    }
}
