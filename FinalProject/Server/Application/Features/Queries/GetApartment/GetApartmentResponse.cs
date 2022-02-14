using System.Collections.Generic;
using Domain.Entities;
using Domain.ViewModels;

namespace Application.Features.Queries.GetApartment
{
    public class GetApartmentResponse
    {
        public int Id { get; set; }
        public char Block { get; set; }
        public bool IsFree { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }
        public OwnerType OwnerType { get; set; }

        public UserVM User { get; set; }
        public List<InvoiceVM> Invoices { get; set; }
    }
}
