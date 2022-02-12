using System;
using Domain.Common;

namespace Domain.Entities
{
    public class Message : BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
