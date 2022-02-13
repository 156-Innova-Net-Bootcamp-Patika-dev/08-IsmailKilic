using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<GetUsersResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public GetUsersHandler(UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<List<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = userManager.Users.ToList();
            return mapper.Map<List<GetUsersResponse>>(users);
        }
    }
}
