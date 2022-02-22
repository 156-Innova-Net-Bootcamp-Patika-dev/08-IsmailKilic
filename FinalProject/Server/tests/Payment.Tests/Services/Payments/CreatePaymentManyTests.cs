using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Exceptions;
using AutoMapper;
using MassTransit;
using Moq;
using PaymentAPI.Data;
using PaymentAPI.Models;
using PaymentAPI.Models.Dtos;
using PaymentAPI.Services;
using Xunit;

namespace Payment.Tests.Services.Payments
{
    public class CreatePaymentManyTests
    {
        private readonly Mock<IMongoRepository<PaymentAPI.Models.Payment>> mockPayment;
        private readonly Mock<IPublishEndpoint> mockPublish;
        private readonly Mock<IMongoRepository<User>> mockUser;
        private readonly Mock<IMongoRepository<Invoice>> mockInvoice;
        private readonly Mock<IMapper> mockMapper;

        public CreatePaymentManyTests()
        {
            mockInvoice = new Mock<IMongoRepository<Invoice>>();
            mockMapper = new Mock<IMapper>();
            mockUser = new Mock<IMongoRepository<User>>();
            mockPublish = new Mock<IPublishEndpoint>();
            mockPayment = new Mock<IMongoRepository<PaymentAPI.Models.Payment>>();
        }

        [Fact]
        public async Task ShouldThrowErrorIfUserNotExisted()
        {
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(value: null);

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            Task act() => service.CreatePaymentMany(new List<CreatePaymentDto> { }, "123");

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Kullanıcı bulunamadı", exception.Message);
            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowErrorIfBalanceLessThanPrice()
        {
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User { Balance = 100 });

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            Task act() => service.CreatePaymentMany(new List<CreatePaymentDto>
            {
                new CreatePaymentDto { Price = 200}
            }, "123");

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Yeterli bakiye bulunamadı. Bakiyeniz 200 TL yükseltildi", exception.Message);
            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreateManyPayments()
        {
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User { Balance = 200 });
            mockPayment.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()))
                .ReturnsAsync(It.IsAny<PaymentAPI.Models.Payment>());
            mockInvoice.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Invoice, bool>>>()))
                .ReturnsAsync(It.IsAny<Invoice>());
            mockUser.Setup(x => x.ReplaceOneAsync(It.IsAny<User>()));
            mockPayment.Setup(x => x.InsertOneAsync(It.IsAny<PaymentAPI.Models.Payment>()));

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            await service.CreatePaymentMany(new List<CreatePaymentDto>
            {
                new CreatePaymentDto { Price = 100},
                new CreatePaymentDto { Price = 50},
            }, "123");

            // assert

            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            mockPayment.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()), Times.AtLeastOnce);
            mockInvoice.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.AtLeastOnce);
        }
    }
}
