using System.Collections.Generic;

namespace Application.Features.Commands.Auth.Login
{
    public class LoginCommandResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public IList<string> Roles { get; set; }
    }
}
