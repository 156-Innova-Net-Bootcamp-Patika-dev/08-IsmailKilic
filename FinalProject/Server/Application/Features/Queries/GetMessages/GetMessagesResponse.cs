using System;
using Domain.ViewModels;

namespace Application.Features.Queries.GetMessages
{
    public class GetMessagesResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserVM Sender { get; set; }
        public UserVM Receiver { get; set; }
    }
}
