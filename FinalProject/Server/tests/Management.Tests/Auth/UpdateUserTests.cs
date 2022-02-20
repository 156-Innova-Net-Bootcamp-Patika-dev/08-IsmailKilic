using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Auth.UpdateUser;
using Application.Interfaces.Cache;
using AutoMapper;
using Shouldly;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace Management.Tests.Auth
{
    public class UpdateUserTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockUserRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICacheService> mockCache;

        public UpdateUserTests()
        {
            mockMapper = new Mock<IMapper>();
            mockUserRepo = RegisterUserManager.GetRegisterUserManager();
            mockCache = MockCacheService.GetCacheService();
        }

        [Fact]
        public async Task ShouldThrowErrorIfCantFindUserById()
        {
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var handler = new UpdateUserHandler(mockUserRepo.Object, mockMapper.Object, mockCache.Object);

            Task act() => handler.Handle(new UpdateUserRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldUpdateUser()
        {
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { });
            mockUserRepo.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
            mockUserRepo.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "User" });

            mockMapper.Setup(x => x.Map<UpdateUserResponse>(It.IsAny<ApplicationUser>()))
                .Returns(new UpdateUserResponse { Email = "test@test.com" });

            var handler = new UpdateUserHandler(mockUserRepo.Object, mockMapper.Object, mockCache.Object);

            var result = await handler.Handle(new UpdateUserRequest { }, CancellationToken.None);

            result.Email.ShouldBe("test@test.com");
            result.Roles.Count.ShouldBe(1);
            result.Roles[0].ShouldBe("User");
        }
    }
}
