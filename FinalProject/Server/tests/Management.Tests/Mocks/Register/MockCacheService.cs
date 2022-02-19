using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Cache;
using Moq;

namespace Management.Tests.Mocks.Register
{
    public static class MockCacheService
    {
        public static Mock<ICacheService> GetCacheService()
        {

            var mgr = new Mock<ICacheService>();
            mgr.Setup(x=>x.Remove(It.IsAny<string>())).Verifiable();

            return mgr;
        }
    }
}
