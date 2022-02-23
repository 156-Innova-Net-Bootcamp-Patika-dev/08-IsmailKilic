using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Commands.Apartments.UpdateApartment
{
    public class UpdateApartmentCommandHandler : IRequestHandler<UpdateApartmentCommandRequest, UpdateApartmentCommandResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly IMapper mapper;

        public UpdateApartmentCommandHandler(IAparmentRepository apartmentRepository, IMapper mapper)
        {
            this.apartmentRepository = apartmentRepository;
            this.mapper = mapper;
        }

        public async Task<UpdateApartmentCommandResponse> Handle(UpdateApartmentCommandRequest request, CancellationToken cancellationToken)
        {
            var existedApt = apartmentRepository.Get(x => x.Id == request.Id);
            if (existedApt == null) throw new BadRequestException("Daire bulunamadı");

            var apt = apartmentRepository.Get(x => x.Block == request.Block && x.Floor == request.Floor
            && x.No == request.No && x.Id != request.Id);
            if (apt != null) throw new BadRequestException("Bu daire daha önce kayıt edilmiş");

            existedApt.Block = request.Block;
            existedApt.Type = request.Type;
            existedApt.Floor = request.Floor;
            existedApt.No = request.No;
            existedApt.OwnerType = request.OwnerType;

            await apartmentRepository.Update(existedApt);
            return new UpdateApartmentCommandResponse { Message = "Güncelleme başarılı" };
        }
    }
}
