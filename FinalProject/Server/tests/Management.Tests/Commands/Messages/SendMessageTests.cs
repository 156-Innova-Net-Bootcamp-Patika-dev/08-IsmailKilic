using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Messages.SendMessage;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Commands.Messages
{
    public class SendMessageTests
    {
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IMessageRepository> mockRepo;
        private readonly Mock<UserManager<ApplicationUser>> mockUserRepo;

        public SendMessageTests()
        {
            mockMapper = new Mock<IMapper>();
            mockRepo = new Mock<IMessageRepository>();
            mockUserRepo = RegisterUserManager.GetRegisterUserManager();
        }

        [Fact]
        public async Task ShouldThrowErrorIfSenderNotExisted()
        {
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsIn<string>("123"))).ReturnsAsync(value: null);

            var handler = new SendMessageHandler(mockMapper.Object, mockRepo.Object, mockUserRepo.Object);

            Task act() => handler.Handle(new SendMessageRequest { SenderId = "123" }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Gönderen kullanıcı bulunamadı", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowErrorIfReceiverNotExisted()
        {
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsIn<string>("123"))).ReturnsAsync(new ApplicationUser { });
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsIn<string>("456"))).ReturnsAsync(value: null);

            var handler = new SendMessageHandler(mockMapper.Object, mockRepo.Object, mockUserRepo.Object);

            Task act() => handler.Handle(new SendMessageRequest { SenderId = "123", ReceiverId = "456" }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Alıcı kullanıcı bulunamadı", exception.Message);
        }

        [Fact]
        public async Task ShouldSendMessageSuccessfully()
        {
            // arrange
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsIn<string>("123"))).ReturnsAsync(new ApplicationUser { });
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsIn<string>("456"))).ReturnsAsync(new ApplicationUser { });
            mockUserRepo.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            mockRepo.Setup(x => x.Add(It.IsAny<Message>())).ReturnsAsync(new Message { Content = "New Message" });

            mockMapper.Setup(x => x.Map<SendMessageResponse>(It.IsAny<Message>()))
               .Returns(new SendMessageResponse { Content = "New Message", IsRead = false });

            var handler = new SendMessageHandler(mockMapper.Object, mockRepo.Object, mockUserRepo.Object);

            var result = await handler.Handle(new SendMessageRequest { SenderId = "123", ReceiverId = "456" }, CancellationToken.None);

            result.Content.ShouldBe("New Message");
            result.IsRead.ShouldBeFalse();
        }
    }
}
