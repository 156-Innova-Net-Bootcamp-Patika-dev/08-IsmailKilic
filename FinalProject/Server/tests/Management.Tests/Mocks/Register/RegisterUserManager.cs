using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Management.Tests.Mocks.Register
{
    public static class RegisterUserManager
    {
        public static Mock<UserManager<ApplicationUser>> GetRegisterUserManager()
        {
            var users = new List<ApplicationUser>()
            {
            };

            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            mgr.Setup(s => s.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

    }
}
