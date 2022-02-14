using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Queries.GetApartment
{
    public class GetApartmentHandler : IRequestHandler<GetApartmentQuery, GetApartmentResponse>
    {
        private readonly IAparmentRepository aparmentRepository;
        private readonly IMapper mapper;

        public GetApartmentHandler(IAparmentRepository aparmentRepository, IMapper mapper)
        {
            this.aparmentRepository = aparmentRepository;
            this.mapper = mapper;
        }

        public async Task<GetApartmentResponse> Handle(GetApartmentQuery request, CancellationToken cancellationToken)
        {
            var apartment = aparmentRepository.Get(x => x.Id == request.Id, x => x.Invoices, x => x.User);
            return mapper.Map<GetApartmentResponse>(apartment);
        }
    }
}
