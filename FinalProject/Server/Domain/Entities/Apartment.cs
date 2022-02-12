using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Apartment : BaseEntity
    {
        public int Id { get; set; }
        public char Block { get; set; }
        public bool IsFree { get; set; }
        public string Type { get; set; }
        public int Floor { get; set; }
        public int No { get; set; }
        public OwnerType OwnerType { get; set; }

        public ApplicationUser User { get; set; }
        public IEnumerable<Invoice> Invoices{ get; set; }
    }

    public enum OwnerType
    {
        Owner,
        Tenant
    }
}
