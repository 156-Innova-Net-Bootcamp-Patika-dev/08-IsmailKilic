using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models.Dtos;
using PaymentAPI.Services;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(CreatePaymentDto dto)
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            dto.UserId = userId;

            await paymentService.CreatePayment(dto);
            return Ok();
        }

        [Authorize]
        [HttpPost("many")]
        public async Task<ActionResult> PostMany(List<CreatePaymentDto> dto)
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;

            await paymentService.CreatePaymentMany(dto, userId);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetPaymentsByUser()
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            return Ok(paymentService.GetPaymentsByUser(userId));
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult GetAllPayments()
        {
            return Ok(paymentService.GetAllPayments());
        }
    }
}
