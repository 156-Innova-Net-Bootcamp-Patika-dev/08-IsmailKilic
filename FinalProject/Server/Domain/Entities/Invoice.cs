using Domain.Common;

namespace Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int Id { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Price { get; set; }
        public bool IsPaid { get; set; }

        public Apartment Apartment { get; set; }
    }

    public enum InvoiceType
    {
        Aidat,
        Elektrik,
        Su,
        DogalGaz
    }
}
