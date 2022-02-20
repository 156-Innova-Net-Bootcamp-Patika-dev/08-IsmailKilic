using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Features.Events;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MassTransit;
using MessageContracts.Events;
using Moq;
using Xunit;

namespace Management.Tests.Events
{
    public class PaymentCreatedConsumerTests
    {
        private readonly Mock<IInvoiceRepository> mockRepo;
        public PaymentCreatedConsumerTests()
        {
            mockRepo = new Mock<IInvoiceRepository>();
        }

        [Fact]
        public async Task Should_return_payment_createdAsync()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(new Invoice { Id = 1 });
            mockRepo.Setup(x => x.Update(It.IsAny<Invoice>())).ReturnsAsync(new Invoice { });

            var consumer = new PaymentCreatedConsumer(mockRepo.Object);

            await consumer.Consume(Mock.Of<ConsumeContext<PaymentCreated>>(x => x.Message.InvoiceId == 1));

            mockRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            mockRepo.Verify(x => x.Update(It.IsAny<Invoice>()), Times.Once);
        }
    }
}
