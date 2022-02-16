using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Commands.Messages.SendMessage;
using Application.Features.Queries.GetMessages;
using Application.Features.Queries.ReadMessage;
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

        [Authorize]
        [HttpPost]
        public async Task<SendMessageResponse> SendMessage(SendMessageRequest request)
        {
            return await mediator.Send(request);
        }

        [Authorize]
        [HttpGet]
        public async Task<List<GetMessagesResponse>> GetMessages()
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            return await mediator.Send(new GetMessagesQuery() { UserId = userId });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ReadMessageResponse> ReadMessage(int id)
        {
            var userId = User.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault()?.Value;
            return await mediator.Send(new ReadMessageQuery() { ReaderId = userId, MessageId = id });
        }
    }
}
