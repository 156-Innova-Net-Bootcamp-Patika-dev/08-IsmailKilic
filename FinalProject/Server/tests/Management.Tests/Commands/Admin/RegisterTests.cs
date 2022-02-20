using Application.Features.Commands.Admin.Register;
using Domain.Entities;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using Management.Tests.Mocks.Register;
using System.Threading.Tasks;
using System.Threading;
using Shouldly;
using System.Linq;
using Application.Interfaces.Cache;
using MassTransit;
using Application.Exceptions;

namespace Management.Tests.Admin
{
    public class RegisterTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockRepo;
        private readonly Mock<ICacheService> mockCache;
        private readonly Mock<IPublishEndpoint> mockPublish;


        public RegisterTests()
        {
            mockRepo = RegisterUserManager.GetRegisterUserManager();
            mockCache = MockCacheService.GetCacheService();
            mockPublish = new Mock<IPublishEndpoint>();
        }

        [Fact]
        public async Task ShouldCreateNewUser()
        {
            var handler = new RegisterCommandHandler(mockRepo.Object, mockPublish.Object, mockCache.Object);

            var result = await handler.Handle(new RegisterCommandRequest { Email = "test@test.com" }, CancellationToken.None);

            result.Succeed.ShouldBe(true);
        }

        [Fact]
        public async Task ShouldThrowsErrorWithUsedUsername()
        {
            mockRepo.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { UserName = "user" });
            var handler = new RegisterCommandHandler(mockRepo.Object, mockPublish.Object, mockCache.Object);

            Task act() => handler.Handle(new RegisterCommandRequest { Email = "test@test.com" }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }

        [Fact]
        public async Task ShouldThrowsErrorWithUsedEmail()
        {
            mockRepo.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Email = "test@test.com" });
            var handler = new RegisterCommandHandler(mockRepo.Object, mockPublish.Object, mockCache.Object);

            Task act() => handler.Handle(new RegisterCommandRequest { Email = "test@test.com" }, CancellationToken.None);

            await Assert.ThrowsAsync<BadRequestException>(act);
        }
    }
}
