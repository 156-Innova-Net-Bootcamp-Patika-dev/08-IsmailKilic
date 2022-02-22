using System;
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
    public class CreatePaymentTests
    {
        private readonly Mock<IMongoRepository<PaymentAPI.Models.Payment>> mockPayment;
        private readonly Mock<IPublishEndpoint> mockPublish;
        private readonly Mock<IMongoRepository<User>> mockUser;
        private readonly Mock<IMongoRepository<Invoice>> mockInvoice;
        private readonly Mock<IMapper> mockMapper;

        public CreatePaymentTests()
        {
            mockInvoice = new Mock<IMongoRepository<Invoice>>();
            mockMapper = new Mock<IMapper>();
            mockUser = new Mock<IMongoRepository<User>>();
            mockPublish = new Mock<IPublishEndpoint>();
            mockPayment = new Mock<IMongoRepository<PaymentAPI.Models.Payment>>();
        }

        [Fact]
        public async Task ShouldThrowErrorIfInvoicePaidBefore()
        {
            mockPayment.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()))
                .ReturnsAsync(new PaymentAPI.Models.Payment { });

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            Task act() => service.CreatePayment(new CreatePaymentDto { });

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Bu fatura daha önce ödenmiş", exception.Message);
        }

        [Fact]
        public async Task ShouldThrowErrorIfUserNotExisted()
        {
            mockPayment.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()))
                .ReturnsAsync(value: null);
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(value: null);

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            Task act() => service.CreatePayment(new CreatePaymentDto { });

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Kullanıcı bulunamadı", exception.Message);
            mockPayment.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()), Times.Once);
            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowErrorIfUserBalanceLessThanInvoicePrice()
        {
            // arrange
            mockPayment.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()))
                .ReturnsAsync(value: null);
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User { Balance = 100 });
            mockUser.Setup(x => x.ReplaceOneAsync(It.IsAny<User>()));

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            // act
            Task act() => service.CreatePayment(new CreatePaymentDto { Price = 200 });

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Yeterli bakiye bulunamadı. Bakiyeniz 200 TL yükseltildi", exception.Message);
            mockPayment.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()), Times.Once);
            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldThrowErrorIfInvoiceNotExisted()
        {
            // arrange
            mockPayment.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()))
                .ReturnsAsync(value: null);
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User { Balance = 200 });
            mockUser.Setup(x => x.ReplaceOneAsync(It.IsAny<User>()));
            mockInvoice.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Invoice, bool>>>())).ReturnsAsync(value: null);

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            // act
            Task act() => service.CreatePayment(new CreatePaymentDto { Price = 100 });

            // assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(act);

            Assert.Equal("Fatura bulunamadı", exception.Message);
            mockPayment.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()), Times.Once);
            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            mockInvoice.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatePayment()
        {
            // arrange
            mockPayment.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()))
                .ReturnsAsync(value: null);
            mockUser.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new User { Balance = 200 });
            mockUser.Setup(x => x.ReplaceOneAsync(It.IsAny<User>()));
            mockInvoice.Setup(x => x.FindOneAsync(It.IsAny<Expression<Func<Invoice, bool>>>())).ReturnsAsync(new Invoice { });
            mockPayment.Setup(x => x.InsertOneAsync(It.IsAny<PaymentAPI.Models.Payment>()));

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            // act
            await service.CreatePayment(new CreatePaymentDto { Price = 100 });

            // assert
            mockPayment.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<PaymentAPI.Models.Payment, bool>>>()), Times.Once);
            mockUser.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Once);
            mockInvoice.Verify(x => x.FindOneAsync(It.IsAny<Expression<Func<Invoice, bool>>>()), Times.Once);
            mockPayment.Verify(x => x.InsertOneAsync(It.IsAny<PaymentAPI.Models.Payment>()), Times.Once);
        }
    }
}
