using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Cache;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Auth.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager,IMapper mapper, ICacheService cacheService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null) throw new BadRequestException("Kullanıcı bulunamadı");
            if (user.IsDelete) throw new BadRequestException("Kullanıcı aktif değil");

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.TCNo = request.TCNo;
            user.LicenseNo = request.LicenseNo;

            await userManager.UpdateAsync(user);

            var newUser = mapper.Map<UpdateUserResponse>(user);
            var userRoles = await userManager.GetRolesAsync(user);

            // clear cache
            cacheService.Remove(CacheConstants.UsersKey);

            newUser.Roles = userRoles;
            return newUser;
        }
    }
}
