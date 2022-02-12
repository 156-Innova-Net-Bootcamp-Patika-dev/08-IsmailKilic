﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Features.Commands.Invoices.CreateInvoice;
using Application.Features.Queries.GetInvoices;
using MediatR;
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

        [HttpPost]
        public async Task<CreateInvoiceResponse> CreateInvoice(CreateInvoiceRequest request)
        {
            return await mediator.Send(request);
        }

        [HttpGet]
        public async Task<List<GetInvoicesResponse>> GetInvoices()
        {
            return await mediator.Send(new GetInvoicesQuery());
        }
    }
}