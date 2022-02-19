using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Cache;
using Domain.Constants;
using Domain.Entities;
using MassTransit;
using MediatR;
using MessageContracts.Events;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Admin.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ICacheService cacheService;

        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, IPublishEndpoint publishEndpoint, ICacheService cacheService)
        {
            this.userManager = userManager;
            this.publishEndpoint = publishEndpoint;
            this.cacheService = cacheService;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
        {
            var userExists = await userManager.FindByNameAsync(request.Username);
            if (userExists != null)
            {
                throw new BadRequestException("This username is already registered");
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

            // varsayılan şifre
            var password = "User*123";
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(result.ToString());

            await userManager.AddToRoleAsync(user, request.Role);
            cacheService.Remove(CacheConstants.UsersKey);

            // publish an event
            _ = publishEndpoint.Publish(new UserCreated
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            });

            return new RegisterCommandResponse
            {
                Succeed = true,
                Message = "User registered successfully"
            };
        }
    }
}
