using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Constants;
using MediatR;

namespace Application.Features.Queries.GetApartments
{
    public class GetApartmentsHandler : IRequestHandler<GetApartmentsQuery, List<GetApartmentsResponse>>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;
        private readonly ICacheService cacheService;

        public GetApartmentsHandler(IAparmentRepository apartmentRepository, IMapper mapper, ICacheService cacheService)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
            this.cacheService = cacheService;
        }

        public async Task<List<GetApartmentsResponse>> Handle(GetApartmentsQuery request, CancellationToken cancellationToken)
        {
            // if user wants his apartments
            if (request.ByUser)
            {
                var list = apartmentRepository.GetList(x => x.User.Id == request.UserId, x => x.User);
                return mapper.Map<List<GetApartmentsResponse>>(list);
            }

            // if admin wants all apartments
            // first check cache
            if (cacheService.Any(CacheConstants.ApartmentsKey))
            {
                var apartmentList = cacheService.Get<List<GetApartmentsResponse>>(CacheConstants.ApartmentsKey);
                return apartmentList;
            }

            var apartments = apartmentRepository.GetList(null, x => x.User);

            var response = mapper.Map<List<GetApartmentsResponse>>(apartments);
            cacheService.Add(CacheConstants.ApartmentsKey, response);

            return response;
        }
    }
}
