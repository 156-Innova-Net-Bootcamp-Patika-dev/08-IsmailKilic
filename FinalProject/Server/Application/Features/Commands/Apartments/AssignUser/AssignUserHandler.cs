using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.Apartments.AssignUser
{
    public class AssignUserHandler : IRequestHandler<AssignUserRequest, AssignUserResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public AssignUserHandler(IAparmentRepository apartmentRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<AssignUserResponse> Handle(AssignUserRequest request, CancellationToken cancellationToken)
        {
            var apartment = apartmentRepository.Get(x => x.Id == request.ApartmentId);
            if (apartment == null) throw new Exception("Daire bulunamadı");

            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            apartment.User = user;
            apartment.IsFree = false;
            apartment.OwnerType = request.OwnerType;

            return mapper.Map<AssignUserResponse>(await apartmentRepository.Update(apartment));
        }
    }
}
