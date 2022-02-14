using MediatR;

namespace Application.Features.Commands.Auth.ChangePassword
{
    public class ChangePasswordRequest : IRequest<ChangePasswordResponse>
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
