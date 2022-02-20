using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetApartment;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetApartmentTests
    {
        private readonly Mock<IAparmentRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        public GetApartmentTests()
        {
            mockRepo = new Mock<IAparmentRepository>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldReturnApartment()
        {
            // arrange
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>(), It.IsAny<Expression<Func<Apartment, object>>[]>()))
                .Returns(new Apartment { Block = 'A', Floor = 1 });

            mockMapper.Setup(x => x.Map<GetApartmentResponse>(It.IsAny<Apartment>()))
               .Returns(new GetApartmentResponse { Block = 'A', Floor = 1 });

            var handler = new GetApartmentHandler(mockRepo.Object, mockMapper.Object);

            // act
            var result = await handler.Handle(new GetApartmentQuery { Id = 1}, CancellationToken.None);

            // assert
            result.Block.ShouldBe('A');
            result.Floor.ShouldBe(1);
            mockRepo.Verify(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>(), It.IsAny<Expression<Func<Apartment, object>>[]>()), Times.Once);
        }
    }
}
