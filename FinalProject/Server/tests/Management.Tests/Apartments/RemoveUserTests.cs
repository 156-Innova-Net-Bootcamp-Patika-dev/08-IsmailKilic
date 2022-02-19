using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.RemoveUser;
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
    public class RemoveUserTests
    {
        private readonly Mock<IAparmentRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICacheService> mockCache;

        public RemoveUserTests()
        {
            mockRepo = new Mock<IAparmentRepository>();
            mockCache = MockCacheService.GetCacheService();
            mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<RemoveUserResponse>(It.IsAny<Apartment>()))
            .Returns(new RemoveUserResponse { IsFree = true });
        }

        [Fact]
        public async Task ShouldThrowErrorIfApartmentNotExisted()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(value: null);

            var handler = new RemoveUserHandler(mockRepo.Object, mockMapper.Object, mockCache.Object);

            Task act() => handler.Handle(new RemoveUserRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldRemoveUserFromApartment()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>(),
                It.IsAny<Expression<Func<Apartment,object>>>())).Returns(new Apartment { Floor = 1, IsFree = true });
            mockRepo.Setup(x => x.Update(It.IsAny<Apartment>())).ReturnsAsync(new Apartment { Floor = 1, IsFree = true });

            var handler = new RemoveUserHandler(mockRepo.Object, mockMapper.Object, mockCache.Object);

            var result = await handler.Handle(new RemoveUserRequest { }, CancellationToken.None);

            result.IsFree.ShouldBe(true);
        }
    }
}
