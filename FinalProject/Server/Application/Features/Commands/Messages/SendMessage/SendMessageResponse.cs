using System;
using Domain.ViewModels;

namespace Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserVM Sender { get; set; }
        public UserVM Receiver { get; set; }
    }
}
