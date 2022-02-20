using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetAllInvoices;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace Management.Tests.Queries
{
    public class GetAllInvoicesTests
    {
        private readonly Mock<IInvoiceRepository> mockRepo;
        private readonly Mock<IMapper> mockMapper;

        public GetAllInvoicesTests()
        {
            mockMapper = new Mock<IMapper>();
            mockRepo = new Mock<IInvoiceRepository>();
        }

        [Fact]
        public async Task ShouldReturnAllInvoices()
        {
            // arrange
            mockRepo.Setup(x => x.GetList(It.IsAny<Expression<Func<Invoice, bool>>>(), It.IsAny<Expression<Func<Invoice, object>>>()))
                .Returns(new List<Invoice>
                {
                    new Invoice{ Id = 1,},
                    new Invoice{ Id = 2,}
                });

            mockMapper.Setup(x => x.Map<List<GetAllInvoicesResponse>>(It.IsAny<List<Invoice>>()))
               .Returns(new List<GetAllInvoicesResponse>
               {
                   new GetAllInvoicesResponse{Id=1},
                   new GetAllInvoicesResponse{Id=2},
               });

            var handler = new GetAllInvoicesHandler(mockRepo.Object, mockMapper.Object);

            // act
            var result = await handler.Handle(new GetAllInvoicesQuery(), CancellationToken.None);

            // assert
            result.Count.ShouldBe(2);
            mockRepo.Verify(x=> x.GetList(It.IsAny<Expression<Func<Invoice, bool>>>(), It.IsAny<Expression<Func<Invoice, object>>>()),Times.Once);
        }
    }
}
