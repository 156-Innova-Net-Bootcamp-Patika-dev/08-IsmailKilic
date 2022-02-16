using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Queries.ReadMessage
{
    public class ReadMessageHandler : IRequestHandler<ReadMessageQuery, ReadMessageResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;

        public ReadMessageHandler(UserManager<ApplicationUser> userManager, IMessageRepository messageRepository,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }
        public async Task<ReadMessageResponse> Handle(ReadMessageQuery request, CancellationToken cancellationToken)
        {
            var receiver = await userManager.FindByIdAsync(request.ReaderId);
            var message = messageRepository.Get(x => x.Id == request.MessageId, x => x.Receiver, x => x.Sender);

            // if message is null, throw bad request exception
            if (message == null) throw new BadRequestException("Mesaj bulunamadı");

            // if reader id equal to message receiver id
            // and message is unread
            // update them
            if(receiver.Id == message.Receiver.Id && !message.IsRead)
            {
                receiver.Unread--;
                await userManager.UpdateAsync(receiver);

                message.IsRead = true;
                await messageRepository.Update(message);
            }

            return mapper.Map<ReadMessageResponse>(message);
        }
    }
}
