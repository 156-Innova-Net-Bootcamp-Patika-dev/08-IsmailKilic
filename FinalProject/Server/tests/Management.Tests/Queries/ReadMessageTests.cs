using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Queries.ReadMessage;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class ReadMessageTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockUserRepo;
        private readonly Mock<IMessageRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        public ReadMessageTests()
        {
            mockMapper = new Mock<IMapper>();
            mockRepo = new Mock<IMessageRepository>();
            mockUserRepo = RegisterUserManager.GetRegisterUserManager();
        }

        [Fact]
        public async Task ShouldThrowErrorIfMessageNotFound()
        {
            // arrange
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Message, bool>>>(), It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(value: null);

            var handler = new ReadMessageHandler(mockUserRepo.Object, mockRepo.Object, mockMapper.Object);

            // act
            Task act()=> handler.Handle(new ReadMessageQuery { }, CancellationToken.None);

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Mesaj bulunamadı", exception.Message);
        }

        [Fact]
        public async Task ShouldReadMessageAndUpdateMessageStatus()
        {
            // arrange
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = "123"});
            mockUserRepo.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Message, bool>>>(), It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(new Message 
                {
                    Receiver = new ApplicationUser { Id = "123" },
                    IsRead = false,
                });
            mockRepo.Setup(x => x.Update(It.IsAny<Message>())).ReturnsAsync(new Message { });

            mockMapper.Setup(x => x.Map<ReadMessageResponse>(It.IsAny<Message>()))
               .Returns(new ReadMessageResponse { IsRead = true, Id = 1});

            var handler = new ReadMessageHandler(mockUserRepo.Object, mockRepo.Object, mockMapper.Object);

            // act
            var result = await handler.Handle(new ReadMessageQuery { }, CancellationToken.None);

            // assert
            result.IsRead.ShouldBeTrue();
            result.Id.ShouldBe(1);
        }
    }
}
