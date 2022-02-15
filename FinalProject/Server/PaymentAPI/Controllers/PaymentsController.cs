using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;

        public PaymentsController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post(int value)
        {
            await publishEndpoint.Publish<PaymentCreated>(new PaymentCreated
            {
                InvoiceId = value
            });

            return Ok();
        }
    }
}
