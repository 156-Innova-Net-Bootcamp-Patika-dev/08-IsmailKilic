using System.Threading.Tasks;
using Application.Features.Commands.Messages.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMediator mediator;

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<SendMessageResponse> CreateInvoice(SendMessageRequest request)
        {
            return await mediator.Send(request);
        }
    }
}
