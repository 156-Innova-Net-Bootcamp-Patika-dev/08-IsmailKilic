using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Invoices.CreateInvoice;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MassTransit;
using Moq;
using Xunit;
using Shouldly;
using Domain.ViewModels;

namespace Management.Tests.Invoices
{
    public class CreateInvoiceTests
    {
        private readonly Mock<IAparmentRepository> mockAptRepo;
        private readonly Mock<IInvoiceRepository> mockInvoiceRepo;
        private readonly Mock<IPublishEndpoint> mockPublish;
        private readonly Mock<IMapper> mockMapper;

        public CreateInvoiceTests()
        {
            mockPublish = new Mock<IPublishEndpoint>();
            mockAptRepo = new Mock<IAparmentRepository>();
            mockInvoiceRepo = new Mock<IInvoiceRepository>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldThrowErrorIfInvoiceAlreadyCreated()
        {
            mockInvoiceRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(new Invoice { });

            var handler = new CreateInvoiceHandler(mockInvoiceRepo.Object, mockMapper.Object, mockAptRepo.Object, mockPublish.Object);

            Task act() => handler.Handle(new CreateInvoiceRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldThrowErrorIfApartmentNotExisted()
        {
            mockInvoiceRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(value: null);
            mockAptRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(value: null);

            var handler = new CreateInvoiceHandler(mockInvoiceRepo.Object, mockMapper.Object, mockAptRepo.Object, mockPublish.Object);

            Task act() => handler.Handle(new CreateInvoiceRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldCreateNewInvoice()
        {
            // arrange
            mockInvoiceRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Invoice, bool>>>())).Returns(value: null);
            mockInvoiceRepo.Setup(x => x.Add(It.IsAny<Invoice>())).ReturnsAsync(new Invoice { Apartment = new Apartment { Id = 1 } });

            mockAptRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(new Apartment { });

            mockMapper.Setup(x => x.Map<Invoice>(It.IsAny<CreateInvoiceRequest>()))
               .Returns(new Invoice { Year = 2022 });

            mockMapper.Setup(x => x.Map<ApartmentVM>(It.IsAny<Apartment>()))
               .Returns(new ApartmentVM { Id = 1 });

            mockMapper.Setup(x => x.Map<CreateInvoiceResponse>(It.IsAny<Invoice>()))
              .Returns(new CreateInvoiceResponse { IsPaid = false, Price = 50, Apartment = new ApartmentVM { Id = 1 } });

            // act
            var handler = new CreateInvoiceHandler(mockInvoiceRepo.Object, mockMapper.Object, mockAptRepo.Object, mockPublish.Object);

            var result = await handler.Handle(new CreateInvoiceRequest { }, CancellationToken.None);

            result.IsPaid.ShouldBe(false);
            result.Price.ShouldBe(50);
        }
    }
}
