using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Auth.Login;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Domain.ViewModels;
using Shouldly;

namespace Management.Tests.Auth
{
    public class LoginCommandTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockUserRepo;
        private readonly Mock<IConfiguration> mockConf;
        private readonly Mock<SignInManager<ApplicationUser>> mockSignInManager;
        private readonly Mock<IMapper> mockMapper;

        public LoginCommandTests()
        {
            mockConf = new Mock<IConfiguration>();
            mockMapper = new Mock<IMapper>();
            mockUserRepo = RegisterUserManager.GetRegisterUserManager();

            mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                mockUserRepo.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            mockMapper.Setup(x => x.Map<UserVM>(It.IsAny<ApplicationUser>()))
           .Returns(new UserVM { Email = "test@test.com" });

            mockConf.SetupGet(x => x[It.Is<string>(s => s == "JWT:Secret")]).Returns("StrONGKAutHENTICATIONKEy");
            mockConf.SetupGet(x => x[It.Is<string>(s => s == "JWT:ValidIssuer")]).Returns("localhost:5000");
            mockConf.SetupGet(x => x[It.Is<string>(s => s == "JWT:ValidAudience")]).Returns("localhost:5000");
        }

        [Fact]
        public async Task ShouldThrowErrorIfCantFindUserByUsername()
        {
            mockUserRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(value: null);

            var handler = new LoginCommandHandler(mockUserRepo.Object, mockConf.Object, mockSignInManager.Object, mockMapper.Object);

            Task act() => handler.Handle(new LoginCommandRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldThrowErrorIfCantProvideCorrectInfos()
        {
            mockUserRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Email = "test@test.com" });

            mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Failed));

            var handler = new LoginCommandHandler(mockUserRepo.Object, mockConf.Object, mockSignInManager.Object, mockMapper.Object);

            Task act() => handler.Handle(new LoginCommandRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldLoginSuccessfully()
        {
            // arrange
            mockUserRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser
            {
                UserName = "test",
                Id = "1234456"
            });

            mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInResult.Success));

            mockUserRepo.Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(
                new List<string>());

            // act
            var handler = new LoginCommandHandler(mockUserRepo.Object, mockConf.Object, mockSignInManager.Object, mockMapper.Object);

            var result = await handler.Handle(new LoginCommandRequest { }, CancellationToken.None);

            result.User.Email.ShouldBe("test@test.com");
        }
    }
}
