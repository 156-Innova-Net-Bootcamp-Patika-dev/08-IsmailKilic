using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Apartments.AssignUser
{
    public class AssignUserHandler : IRequestHandler<AssignUserRequest, AssignUserResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public AssignUserHandler(IAparmentRepository apartmentRepository, IMapper mapper, 
            UserManager<ApplicationUser> userManager, ICacheService cacheService)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.cacheService = cacheService;
        }

        public async Task<AssignUserResponse> Handle(AssignUserRequest request, CancellationToken cancellationToken)
        {
            // find and check if apartment exist
            var apartment = apartmentRepository.Get(x => x.Id == request.ApartmentId);
            if (apartment == null) throw new BadRequestException("Daire bulunamadı");

            // check if apartment is free
            if(apartment.IsFree == false) throw new BadRequestException("Bu daire dolu");

            // find user
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new BadRequestException("Kullanıcı bulunamadı");

            // assign user to apartment
            apartment.User = user;
            apartment.IsFree = false;
            apartment.OwnerType = request.OwnerType;

            // clear cache
            cacheService.Remove(CacheConstants.ApartmentsKey);

            // return response
            return mapper.Map<AssignUserResponse>(await apartmentRepository.Update(apartment));
        }
    }
}
