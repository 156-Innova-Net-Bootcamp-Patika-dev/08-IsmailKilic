using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Apartments
{
    public class AssignUserTests
    {
        private readonly Mock<IAparmentRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICacheService> mockCache;
        private readonly Mock<UserManager<ApplicationUser>> mockUserRepo;


        public AssignUserTests()
        {
            mockRepo = new Mock<IAparmentRepository>();
            mockCache = MockCacheService.GetCacheService();
            mockMapper = new Mock<IMapper>();
            mockUserRepo = RegisterUserManager.GetRegisterUserManager();

            mockMapper.Setup(x => x.Map<Apartment>(It.IsAny<AssignUserRequest>()))
            .Returns(new Apartment { });

            mockMapper.Setup(x => x.Map<AssignUserResponse>(It.IsAny<Apartment>()))
            .Returns(new AssignUserResponse { Block = 'A', IsFree = false });
        }

        [Fact]
        public async Task ShouldThrowErrorIfApartmentNotExisted()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(value: null);

            var handler = new AssignUserHandler(mockRepo.Object, mockMapper.Object, mockUserRepo.Object, mockCache.Object);

            Task act() => handler.Handle(new AssignUserRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldThrowErrorIfApartmentIsNotFree()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(new Apartment { IsFree = false });

            var handler = new AssignUserHandler(mockRepo.Object, mockMapper.Object, mockUserRepo.Object, mockCache.Object);

            Task act() => handler.Handle(new AssignUserRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldThrowErrorIfUserNotExisted()
        {
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(value: null);
            var handler = new AssignUserHandler(mockRepo.Object, mockMapper.Object, mockUserRepo.Object, mockCache.Object);

            Task act() => handler.Handle(new AssignUserRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldAssignUserToApartment()
        {
            mockRepo.Setup(x => x.Get(It.IsAny<Expression<Func<Apartment, bool>>>())).Returns(new Apartment { Floor = 1, IsFree = true });
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { UserName = "test" });
            mockRepo.Setup(x => x.Update(It.IsAny<Apartment>())).ReturnsAsync(new Apartment { Floor = 1, IsFree = false });

            var handler = new AssignUserHandler(mockRepo.Object, mockMapper.Object, mockUserRepo.Object, mockCache.Object);

            var result = await handler.Handle(new AssignUserRequest { }, CancellationToken.None);

            result.Block.ShouldBe('A');
            result.IsFree.ShouldBe(false);
        }
    }
}
