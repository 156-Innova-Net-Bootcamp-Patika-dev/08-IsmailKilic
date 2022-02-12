using MediatR;

namespace Application.Features.Commands.Auth.Login
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
