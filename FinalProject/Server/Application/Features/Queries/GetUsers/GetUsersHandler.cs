using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Cache;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Queries.GetUsers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<GetUsersResponse>>
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public GetUsersHandler(UserManager<ApplicationUser> userManager, IMapper mapper, ICacheService cacheService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<List<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (cacheService.Any(CacheConstants.UsersKey))
            {
                var userList = cacheService.Get<List<GetUsersResponse>>(CacheConstants.UsersKey);
                return userList;
            }

            var users = userManager.Users.ToList();
            var response = mapper.Map<List<GetUsersResponse>>(users);
            cacheService.Add(CacheConstants.UsersKey, response);

            return response;
        }
    }
}
