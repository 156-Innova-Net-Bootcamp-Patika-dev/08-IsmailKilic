using System.Collections.Generic;

namespace Application.Features.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TCNo { get; set; }
        public bool IsDelete { get; set; }
        public string LicenseNo { get; set; }
        public int Unread { get; set; }
        public IList<string> Roles { get; set; }
    }
}
