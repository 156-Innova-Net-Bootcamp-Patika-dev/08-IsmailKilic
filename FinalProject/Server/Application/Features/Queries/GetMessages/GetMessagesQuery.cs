using System.Collections.Generic;
using MediatR;

namespace Application.Features.Queries.GetMessages
{
    public class GetMessagesQuery : IRequest<List<GetMessagesResponse>>
    {
        public string UserId { get; set; }
    }
}
