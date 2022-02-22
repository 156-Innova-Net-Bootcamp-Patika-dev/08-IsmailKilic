using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Admin.ToggleDelete
{
    public class ToggleDeleteCommandResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TCNo { get; set; }
        public bool IsDelete { get; set; }
        public int Unread { get; set; }
        public string LicenseNo { get; set; }
        public IList<string> Roles { get; set; }
    }
}
