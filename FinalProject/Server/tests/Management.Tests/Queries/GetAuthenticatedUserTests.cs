using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Queries.GetAuthenticatedUser;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetAuthenticatedUserTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        public GetAuthenticatedUserTests()
        {
            mockRepo = RegisterUserManager.GetRegisterUserManager();
            mockMapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task ShouldThrowNotFoundErrorIfUserNotFound()
        {
            // arrange
            mockRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var handler = new GetAuthenticatedUserHandler(mockRepo.Object, mockMapper.Object);

            // act
            Task act() => handler.Handle(new GetAuthenticatedUserQuery { Id = "123" }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<NotFoundException>(act);

            // assert
            Assert.Equal("Kullanıcı bulunamadı", exception.Message);
            mockRepo.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ShouldReturnAuthenticatedUser()
        {
            // arrange
            mockRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { });
            mockRepo.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "User" });

            mockMapper.Setup(x => x.Map<GetAuthenticatedUserResponse>(It.IsAny<ApplicationUser>()))
               .Returns(new GetAuthenticatedUserResponse { Email = "test@test.com", Roles = new List<string> { "User" } });

            var handler = new GetAuthenticatedUserHandler(mockRepo.Object, mockMapper.Object);

            // act
            var result = await handler.Handle(new GetAuthenticatedUserQuery { Id = "123" }, CancellationToken.None);

            // assert
            mockRepo.Verify(x => x.FindByIdAsync(It.IsAny<string>()), Times.Once);
            mockRepo.Verify(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()), Times.Once);
            result.Roles[0].ShouldBe("User");
            result.Email.ShouldBe("test@test.com");
        }

    }
}
