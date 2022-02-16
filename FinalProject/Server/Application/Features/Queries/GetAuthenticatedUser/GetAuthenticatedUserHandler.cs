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

namespace Application.Features.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserHandler : IRequestHandler<GetAuthenticatedUserQuery, GetAuthenticatedUserResponse>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public GetAuthenticatedUserHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<GetAuthenticatedUserResponse> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null) throw new NotFoundException("Kullanıcı bulunamadı");

            return mapper.Map<GetAuthenticatedUserResponse>(user);
        }
    }
}
