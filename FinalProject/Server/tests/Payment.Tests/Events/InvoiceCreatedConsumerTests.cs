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
    public class InvoiceCreatedConsumerTests
    {
        private readonly Mock<IMongoRepository<Invoice>> mockRepo;

        public InvoiceCreatedConsumerTests()
        {
            mockRepo = new Mock<IMongoRepository<Invoice>>();   
        }

        [Fact]
        public async Task ShouldCreateInvoiceWhenEventPublished()
        {
            mockRepo.Setup(x => x.InsertOneAsync(It.IsAny<Invoice>()));

            var consumer = new InvoiceCreatedConsumer(mockRepo.Object);

            await consumer.Consume(Mock.Of<ConsumeContext<InvoiceCreated>>(x => x.Message.InvoiceId == 1));

            mockRepo.Verify(x => x.InsertOneAsync(It.IsAny<Invoice>()), Times.Once);
        }
    }
}
