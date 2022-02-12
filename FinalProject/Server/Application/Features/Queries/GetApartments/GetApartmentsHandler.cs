using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Queries.GetApartments
{
    public class GetApartmentsHandler : IRequestHandler<GetApartmentsQuery, List<GetApartmentsResponse>>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;

        public GetApartmentsHandler(IAparmentRepository apartmentRepository,IMapper mapper)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
        }

        public async Task<List<GetApartmentsResponse>> Handle(GetApartmentsQuery request, CancellationToken cancellationToken)
        {
            var apartments = apartmentRepository.GetList(null,x=>x.User);
            return mapper.Map<List<GetApartmentsResponse>>(apartments);
        }
    }
}
