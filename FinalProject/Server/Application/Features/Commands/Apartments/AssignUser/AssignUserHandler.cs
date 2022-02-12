using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Commands.Apartments.AssignUser
{
    public class AssignUserHandler : IRequestHandler<AssignUserRequest, AssignUserResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;

        public AssignUserHandler(IAparmentRepository apartmentRepository, IMapper mapper)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
        }

        public async Task<AssignUserResponse> Handle(AssignUserRequest request, CancellationToken cancellationToken)
        {
            var apartment = apartmentRepository.Get(x => x.Id == request.ApartmentId);
            apartment.UserId = request.UserId;
            apartment.IsFree = false;
            apartment.OwnerType = request.OwnerType;

            return mapper.Map<AssignUserResponse>(await apartmentRepository.Update(apartment));
        }
    }
}
