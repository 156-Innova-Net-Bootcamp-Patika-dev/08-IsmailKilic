using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Auth.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ChangePasswordResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ChangePasswordHandler(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<ChangePasswordResponse> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            var result = await userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

            if (!result.Succeeded) throw new BadRequestException("Hatalı şifre girişi");
            return new ChangePasswordResponse { Message = "Şifre değiştirme işlemi başarılı" };
        }
    }
}
