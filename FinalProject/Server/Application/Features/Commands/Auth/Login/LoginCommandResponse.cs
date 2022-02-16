using System.Collections.Generic;
using Domain.ViewModels;

namespace Application.Features.Commands.Auth.Login
{
    public class LoginCommandResponse
    {
        public string Token { get; set; }
        public string Message { get; set; }
        public UserVM User { get; set; }
    }
}
