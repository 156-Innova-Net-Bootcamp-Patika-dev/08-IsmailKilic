using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Commands.Invoices.CreateInvoice;
using Application.Features.Commands.Invoices.CreateManyInvoices;
using Application.Features.Queries.GetAllInvoices;
using Application.Features.Queries.GetInvoicesByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator mediator;

        public InvoicesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<CreateInvoiceResponse> CreateInvoice(CreateInvoiceRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("many")]
        public async Task<CreateManyInvoicesResponse> CreateInvoices(CreateManyInvoicesRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpGet]
        public async Task<List<GetInvoicesResponse>> GetInvoices()
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;

            return await mediator.Send(new GetInvoicesQuery() { UserId = userId });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<List<GetAllInvoicesResponse>> GetAllInvoices()
        {

            return await mediator.Send(new GetAllInvoicesQuery());
        }
    }
}
