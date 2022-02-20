using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetApartment;
using Application.Features.Queries.GetApartments;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetApartmentsTests
    {
        private readonly Mock<IAparmentRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICacheService> mockCache;

        public GetApartmentsTests()
        {
            mockCache = MockCacheService.GetCacheService();
            mockRepo = new Mock<IAparmentRepository>();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldReturnAllApartments()
        {
            // arrange
            mockRepo.Setup(x => x.GetList(It.IsAny<Expression<Func<Apartment, bool>>>(), It.IsAny<Expression<Func<Apartment, object>>[]>()))
                .Returns(new List<Apartment>
                {
                    new Apartment { Id = 1,}
                });

            mockMapper.Setup(x => x.Map<List<GetApartmentsResponse>>(It.IsAny<List<Apartment>>()))
               .Returns(new List<GetApartmentsResponse>
               {
                   new GetApartmentsResponse { Id = 1,}
               });

            var handler = new GetApartmentsHandler(mockRepo.Object, mockMapper.Object, mockCache.Object);

            // act
            var result = await handler.Handle(new GetApartmentsQuery {  }, CancellationToken.None);

            // assert
            result.Count.ShouldBe(1);
            mockRepo.Verify(x => x.GetList(It.IsAny<Expression<Func<Apartment, bool>>>(), It.IsAny<Expression<Func<Apartment, object>>[]>()),
                Times.Once);
        }
    }
}
