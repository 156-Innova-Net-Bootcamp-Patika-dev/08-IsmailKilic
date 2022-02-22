using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageHandler : IRequestHandler<SendMessageRequest, SendMessageResponse>
    {
        private readonly IMapper mapper;
        private readonly IMessageRepository messageRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public SendMessageHandler(IMapper mapper, IMessageRepository messageRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.mapper = mapper;
            this.messageRepository = messageRepository;
            this.userManager = userManager;
        }

        public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            // Check if sender exist
            var sender = await userManager.FindByIdAsync(request.SenderId);
            if (sender == null) throw new BadRequestException("Gönderen kullanıcı bulunamadı");
            if (sender.IsDelete) throw new BadRequestException("Gönderen kullanıcı aktif değil");

            // Check if receiver exist
            var receiver = await userManager.FindByIdAsync(request.ReceiverId);
            if (receiver == null) throw new BadRequestException("Alıcı kullanıcı bulunamadı");
            if (receiver.IsDelete) throw new BadRequestException("Alıcı kullanıcı aktif değil");
            receiver.Unread++;

            await userManager.UpdateAsync(receiver);

            var message = new Message
            {
                Content = request.Content,
                IsRead = false,
                Receiver = receiver,
                Sender = sender,
                CreatedAt = DateTime.Now
            };

            var sendingMessage = await messageRepository.Add(message);
            return mapper.Map<SendMessageResponse>(sendingMessage);
        }
    }
}
