using System.Collections.Generic;

namespace Application.Features.Commands.Auth.UpdateUser
{
    public class UpdateUserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TCNo { get; set; }
        public string LicenseNo { get; set; }
        public IList<string> Roles { get; set; }
    }
}
