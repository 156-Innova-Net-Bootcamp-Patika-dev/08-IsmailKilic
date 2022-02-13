using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Commands.Messages.SendMessage;
using Application.Features.Queries.GetMessages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<List<GetMessagesResponse>> GetMessages()
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            return await mediator.Send(new GetMessagesQuery() { UserId = userId });
        }
    }
}
