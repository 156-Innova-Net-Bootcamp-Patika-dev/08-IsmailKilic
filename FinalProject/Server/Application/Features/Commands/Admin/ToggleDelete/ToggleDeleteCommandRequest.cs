using MediatR;

namespace Application.Features.Commands.Admin.ToggleDelete
{
    public class ToggleDeleteCommandRequest : IRequest<ToggleDeleteCommandResponse>
    {
        public string UserId { get; set; }
    }
}
