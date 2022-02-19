using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Apartments.CreateApartment
{
    public class CreateApartmentHandler : IRequestHandler<CreateApartmentRequest, CreateApartmentResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public CreateApartmentHandler(IAparmentRepository apartmentRepository, IMapper mapper, ICacheService cacheService)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<CreateApartmentResponse> Handle(CreateApartmentRequest request, CancellationToken cancellationToken)
        {
            var existedApt = apartmentRepository.Get(x => x.Block == request.Block && x.Floor == request.Floor 
            && x.No == request.No);
            if (existedApt != null) throw new BadRequestException("Bu daire daha önce kayıt edilmiş");

            var apartment = mapper.Map<Apartment>(request);
            apartment.IsFree = true;

            var newApartment = await apartmentRepository.Add(apartment);
            cacheService.Remove(CacheConstants.ApartmentsKey);

            return mapper.Map<CreateApartmentResponse>(newApartment);
        }
    }
}
