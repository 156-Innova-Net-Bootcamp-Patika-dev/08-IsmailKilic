using MediatR;

namespace Application.Features.Queries.ReadMessage
{
    public class ReadMessageQuery : IRequest<ReadMessageResponse>
    {
        public string ReaderId { get; set; }
        public int MessageId { get; set; }
    }
}
