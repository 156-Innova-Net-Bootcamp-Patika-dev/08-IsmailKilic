using System;

namespace PaymentAPI.Models.ViewModels
{
    public class PaymentVM
    {
        public string Id { get; set; }
        public int ApartmentId { get; set; }
        public string Last4Number { get; set; }
        public double Price { get; set; }
        public Invoice Invoice { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
