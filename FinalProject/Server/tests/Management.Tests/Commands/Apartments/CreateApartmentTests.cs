using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Apartments
{
    public class CreateApartmentTests
    {
        private readonly Mock<IAparmentRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICacheService> mockCache;


        public CreateApartmentTests()
        {
            mockRepo = new Mock<IAparmentRepository>();
            mockCache = MockCacheService.GetCacheService();
            mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<Apartment>(It.IsAny<CreateApartmentRequest>()))
            .Returns(new Apartment { IsFree = true });
        }

        [Fact]
        public async Task ShouldCreateNewApartment()
        {

            mockMapper.Setup(x => x.Map<CreateApartmentResponse>(It.IsAny<Apartment>()))
            .Returns(new CreateApartmentResponse { Type = "3+1" });

            mockRepo.Setup(x => x.Add(It.IsIn<Apartment>())).ReturnsAsync(new Apartment { Type = "3+1" });

            var handler = new CreateApartmentHandler(mockRepo.Object, mockMapper.Object, mockCache.Object);

            var result = await handler.Handle(new CreateApartmentRequest { }, CancellationToken.None);

            result.Type.ShouldBe("3+1");
        }

        [Fact]
        public async Task ShouldThrowErrorIfApartmentRegistered()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(new Apartment
            {
                Block = 'A'
            });

            var handler = new CreateApartmentHandler(mockRepo.Object, mockMapper.Object, mockCache.Object);

            Task act() => handler.Handle(new CreateApartmentRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }
    }
}
