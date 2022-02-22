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

namespace Application.Features.Commands.Admin.ToggleDelete
{
    public class ToggleDeleteCommandHandler : IRequestHandler<ToggleDeleteCommandRequest, ToggleDeleteCommandResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;


        public ToggleDeleteCommandHandler(UserManager<ApplicationUser> userManager, ICacheService cacheService, IMapper mapper)
        {
            this.userManager = userManager;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public async Task<ToggleDeleteCommandResponse> Handle(ToggleDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new BadRequestException("Kullanıcı bulunamadı");
            
            var userRoles = await userManager.GetRolesAsync(user);

            if(userRoles.Contains("Admin")) throw new BadRequestException("Admin kullanıcısını silemezsiniz"); ;

            user.IsDelete = !user.IsDelete;
            await userManager.UpdateAsync(user);

            var newUser = mapper.Map<ToggleDeleteCommandResponse>(user);

            // clear cache
            cacheService.Remove(CacheConstants.UsersKey);

            newUser.Roles = userRoles;
            return newUser;
        }
    }
}
