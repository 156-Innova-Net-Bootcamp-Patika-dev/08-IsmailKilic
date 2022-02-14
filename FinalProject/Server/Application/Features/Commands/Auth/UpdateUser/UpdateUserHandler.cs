using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Auth.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null) throw new BadRequestException("Kullanıcı bulunamadı");

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.TCNo = request.TCNo;
            user.LicenseNo = request.LicenseNo;

            await userManager.UpdateAsync(user);
            
            return mapper.Map<UpdateUserResponse>(user);
        }
    }
}
