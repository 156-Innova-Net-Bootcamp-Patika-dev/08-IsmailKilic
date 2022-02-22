using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Moq;
using PaymentAPI.Data;
using PaymentAPI.Models;
using PaymentAPI.Models.ViewModels;
using PaymentAPI.Services;
using Xunit;

namespace Payment.Tests.Services.Payments
{
    public class GetAllPaymentsTests
    {
        private readonly Mock<IMongoRepository<PaymentAPI.Models.Payment>> mockPayment;
        private readonly Mock<IPublishEndpoint> mockPublish;
        private readonly Mock<IMongoRepository<User>> mockUser;
        private readonly Mock<IMongoRepository<Invoice>> mockInvoice;
        private readonly Mock<IMapper> mockMapper;

        public GetAllPaymentsTests()
        {
            mockInvoice = new Mock<IMongoRepository<Invoice>>();
            mockMapper = new Mock<IMapper>();
            mockUser = new Mock<IMongoRepository<User>>();
            mockPublish = new Mock<IPublishEndpoint>();
            mockPayment = new Mock<IMongoRepository<PaymentAPI.Models.Payment>>();
        }

        [Fact]
        public async Task ShouldReturnAllPayments()
        {
            mockPayment.Setup(x => x.AsQueryable());
            mockMapper.Setup(x => x.Map<List<PaymentVM>>(It.IsAny<List<PaymentAPI.Models.Payment>>()))
               .Returns(new List<PaymentVM>
               {
                   new PaymentVM{},
                   new PaymentVM{},
               });

            var service = new PaymentManager(mockPayment.Object, mockPublish.Object, mockUser.Object, mockInvoice.Object, mockMapper.Object);

            await service.GetAllPayments();

            // assert
            mockPayment.Verify(x => x.AsQueryable(), Times.Once);
            mockMapper.Verify(x => x.Map<List<PaymentVM>>(It.IsAny<List<PaymentAPI.Models.Payment>>()), Times.Once);
        }
    }
}
