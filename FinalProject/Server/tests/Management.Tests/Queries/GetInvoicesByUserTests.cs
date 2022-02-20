using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetInvoicesByUser;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetInvoicesByUserTests
    {
        private readonly Mock<IInvoiceRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        public GetInvoicesByUserTests()
        {
            mockMapper = new Mock<IMapper>();
            mockRepo = new Mock<IInvoiceRepository>();
        }

        [Fact]
        public async Task ShouldReturnAllInvoicesByUser()
        {
            // arrange
            mockRepo.Setup(x => x.GetList(It.IsAny<Expression<Func<Invoice, bool>>>(), It.IsAny<Expression<Func<Invoice, object>>[]>()))
                .Returns(new List<Invoice> { });

            mockMapper.Setup(x => x.Map<List<GetInvoicesResponse>>(It.IsAny<List<Invoice>>()))
               .Returns(new List<GetInvoicesResponse>
               {
                   new GetInvoicesResponse { Id = 1,}
               });

            var handler = new GetInvoicesHandler(mockRepo.Object, mockMapper.Object);

            // act
            var result = await handler.Handle(new GetInvoicesQuery { }, CancellationToken.None);

            // assert
            result.Count.ShouldBe(1);
            mockRepo.Verify(x => x.GetList(It.IsAny<Expression<Func<Invoice, bool>>>(), It.IsAny<Expression<Func<Invoice, object>>[]>()),
                Times.Once);
        }
    }
}
