using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetMessages;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetMessagesTests
    {
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IMessageRepository> mockRepo;

        public GetMessagesTests()
        {
            mockRepo = new Mock<IMessageRepository>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldReturnMessages()
        {
            // arrange
            mockRepo.Setup(x => x.GetList(It.IsAny<Expression<Func<Message, bool>>>(), It.IsAny<Expression<Func<Message, object>>[]>()))
                .Returns(new List<Message> 
                { 
                    new Message { Id = 1,}
                });

            mockMapper.Setup(x => x.Map<List<GetMessagesResponse>>(It.IsAny<IOrderedEnumerable<Message>>()))
               .Returns(new List<GetMessagesResponse>
               {
                   new GetMessagesResponse { Id = 1,}
               });

            var handler = new GetMessagesHandler(mockMapper.Object, mockRepo.Object);

            // act
            var result = await handler.Handle(new GetMessagesQuery { }, CancellationToken.None);

            // assert
            result.Count.ShouldBe(1);
            mockRepo.Verify(x => x.GetList(It.IsAny<Expression<Func<Message, bool>>>(), It.IsAny<Expression<Func<Message, object>>[]>()),
                Times.Once);
        }
    }
}
