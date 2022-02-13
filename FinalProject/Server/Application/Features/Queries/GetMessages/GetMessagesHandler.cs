using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Queries.GetMessages
{
    public class GetMessagesHandler : IRequestHandler<GetMessagesQuery, List<GetMessagesResponse>>
    {
        private readonly IMapper mapper;
        private readonly IMessageRepository messageRepository;
        //private readonly UserManager<ApplicationUser> userManager;

        public GetMessagesHandler(IMapper mapper, IMessageRepository messageRepository)
        {
            this.mapper = mapper;
            this.messageRepository = messageRepository;
        }

        public async Task<List<GetMessagesResponse>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
        {
            var messages = messageRepository.GetList(x => x.Receiver.Id == request.UserId || x.Sender.Id == request.UserId,
                x => x.Receiver, x => x.Sender).OrderByDescending(x => x.CreatedAt);
            return mapper.Map<List<GetMessagesResponse>>(messages);
        }
    }
}
