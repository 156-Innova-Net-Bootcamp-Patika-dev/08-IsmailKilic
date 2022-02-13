using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Admin.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var userExists = await userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                throw new Exception("This username is already registered");
            }

            ApplicationUser user = new()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username,
                PhoneNumber = request.Phone,
                FullName = request.Fullname,
                TCNo = request.TCNo,
                LicenseNo = request.LicenseNo
            };

            // burada rastgele şifre oluşturulup mail ile kullanıcıya gönderilecek
            var password = "Admin*123";
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(result.ToString());

            await userManager.AddToRoleAsync(user, request.Role);

            return new RegisterCommandResponse
            {
                Succeed = true,
                Message = "User registered successfully"
            };
        }
    }
}
