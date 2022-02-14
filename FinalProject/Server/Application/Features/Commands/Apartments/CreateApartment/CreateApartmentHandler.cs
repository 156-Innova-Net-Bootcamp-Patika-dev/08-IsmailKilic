﻿using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.Apartments.CreateApartment
{
    public class CreateApartmentHandler : IRequestHandler<CreateApartmentRequest, CreateApartmentResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;

        public CreateApartmentHandler(IAparmentRepository apartmentRepository, IMapper mapper)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
        }

        public async Task<CreateApartmentResponse> Handle(CreateApartmentRequest request, CancellationToken cancellationToken)
        {
            var existedApt = apartmentRepository.Get(x => x.Block == request.Block && x.Floor == request.Floor 
            && x.No == request.No);
            if (existedApt != null) throw new BadRequestException("Bu daire daha önce kayıt edilmiş");

            var apartment = mapper.Map<Apartment>(request);
            apartment.IsFree = true;

            var newApartment = await apartmentRepository.Add(apartment);

            return mapper.Map<CreateApartmentResponse>(newApartment);
        }
    }
}
