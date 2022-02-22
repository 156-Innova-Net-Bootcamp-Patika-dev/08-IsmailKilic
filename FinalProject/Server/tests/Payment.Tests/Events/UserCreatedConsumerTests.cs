using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using Moq;
using PaymentAPI.Consumers;
using PaymentAPI.Data;
using PaymentAPI.Models;
using Xunit;

namespace Payment.Tests.Events
{
    public class UserCreatedConsumerTests
    {
        private readonly Mock<IMongoRepository<User>> mockRepo;

        public UserCreatedConsumerTests()
        {
            mockRepo = new Mock<IMongoRepository<User>>();
        }

        [Fact]
        public async Task ShouldCreateUserWhenEventPublished()
        {
            mockRepo.Setup(x => x.InsertOneAsync(It.IsAny<User>()));

            var consumer = new UserCreatedConsumer(mockRepo.Object);

            await consumer.Consume(Mock.Of<ConsumeContext<UserCreated>>(x => x.Message.UserId == "123"));

            mockRepo.Verify(x => x.InsertOneAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
