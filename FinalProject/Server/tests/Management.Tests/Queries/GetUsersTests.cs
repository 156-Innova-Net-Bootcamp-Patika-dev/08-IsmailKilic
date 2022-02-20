using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetUsers;
using Application.Interfaces.Cache;
using AutoMapper;
using Domain.Entities;
using Management.Tests.Mocks.Register;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetUsersTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockRepo;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ICacheService> mockCache;

        public GetUsersTests()
        {
            mockRepo = RegisterUserManager.GetRegisterUserManager();
            mockMapper = new Mock<IMapper>();
            mockCache = MockCacheService.GetCacheService();
        }

        [Fact]
        public async Task ShouldReturnAllUsers()
        {
            // arrange
            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1",  },
                new ApplicationUser { Id = "2",  },
            }.AsQueryable();
            mockRepo.Setup(x => x.Users).Returns(users);

            mockMapper.Setup(x => x.Map<List<GetUsersResponse>>(It.IsAny<List<ApplicationUser>>()))
               .Returns(new List<GetUsersResponse>
               {
                   new GetUsersResponse {  Id = "1"},
                   new GetUsersResponse {  Id = "2"},
               });

            var handler = new GetUsersHandler(mockRepo.Object, mockMapper.Object, mockCache.Object);

            // act
            var result = await handler.Handle(new GetUsersQuery { }, CancellationToken.None);

            // assert
            result.Count.ShouldBe(2);
            result[0].Id.ShouldBe("1");
            result[1].Id.ShouldBe("2");

            mockRepo.Verify(x => x.Users, Times.Once);
        }
    }
}
