using MediatR;

namespace Application.Features.Commands.Auth.UpdateUser
{
    public class UpdateUserRequest : IRequest<UpdateUserResponse>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string TCNo { get; set; }
        public string LicenseNo { get; set; }
    }
}
