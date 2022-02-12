using MediatR;

namespace Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageRequest : IRequest<SendMessageResponse>
    {
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
