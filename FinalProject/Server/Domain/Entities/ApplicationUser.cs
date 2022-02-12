using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser, BaseEntity
    {
        public string FullName { get; set; }
        public string TCNo { get; set; }
        public string LicenseNo { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; }
        [InverseProperty("Sender")]
        public IEnumerable<Message> SendingMessages { get; set; }
        [InverseProperty("Receiver")]
        public IEnumerable<Message> ReceivingMessages { get; set; }
    }
}
