using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Commands.Apartments.RemoveUser
{
    internal class RemoveUserHandler : IRequestHandler<RemoveUserRequest, RemoveUserResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;

        public RemoveUserHandler(IAparmentRepository apartmentRepository, IMapper mapper)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
        }

        public async Task<RemoveUserResponse> Handle(RemoveUserRequest request, CancellationToken cancellationToken)
        {
            var apartment = apartmentRepository.Get(x => x.Id == request.ApartmentId);
            if (apartment == null) throw new Exception("Daire bulunamadı");

            apartment.UserId = null;
            apartment.IsFree = true;

            return mapper.Map<RemoveUserResponse>(await apartmentRepository.Update(apartment));
        }
    }

}