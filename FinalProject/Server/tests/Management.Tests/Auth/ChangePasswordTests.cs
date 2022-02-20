using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.Commands.Auth.ChangePassword;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Auth
{
    public class ChangePasswordTests
    {
        private Mock<UserManager<ApplicationUser>> mockUserRepo;

        public ChangePasswordTests()
        {
            mockUserRepo = RegisterUserManager.GetRegisterUserManager();
            mockUserRepo.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser
            {
                UserName = "test"
            });
        }

        [Fact]
        public async Task ShouldThrowErrorIfPasswordsWrong()
        {
            mockUserRepo.Setup(x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(),It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());

            var handler = new ChangePasswordHandler(mockUserRepo.Object);

            Task act() => handler.Handle(new ChangePasswordRequest { }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldChangeUserPassword()
        {
            mockUserRepo.Setup(x => x.ChangePasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            var handler = new ChangePasswordHandler(mockUserRepo.Object);

            var result = await handler.Handle(new ChangePasswordRequest { }, CancellationToken.None);

            result.Message.ShouldBe("Şifre değiştirme işlemi başarılı");
        }
    }
}