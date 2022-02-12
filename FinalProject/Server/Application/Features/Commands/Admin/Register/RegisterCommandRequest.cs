using MediatR;

namespace Application.Features.Commands.Admin.Register
{
    public class RegisterCommandRequest : IRequest<RegisterCommandResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string TCNo { get; set; }
        public string Phone { get; set; }
        public string LicenseNo { get; set; }
    }
}
