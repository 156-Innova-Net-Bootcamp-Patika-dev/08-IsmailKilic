using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Constants;
using MediatR;

namespace Application.Features.Commands.Apartments.RemoveUser
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public RemoveUserHandler(IAparmentRepository apartmentRepository, IMapper mapper, ICacheService cacheService)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
        {
            var apartment = apartmentRepository.Get(x => x.Id == request.ApartmentId, x => x.User);
            if (apartment == null) throw new BadRequestException("Daire bulunamadı");

            apartment.User = null;
            apartment.IsFree = true;

            // clear cache
            cacheService.Remove(CacheConstants.ApartmentsKey);

            return mapper.Map<RemoveUserResponse>(await apartmentRepository.Update(apartment));
        }
    }
}