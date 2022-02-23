using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces.Cache;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Constants;
using MediatR;

namespace Application.Features.Commands.Apartments.DeleteApartment
{
    public class DeleteApartmentCommandHandler : IRequestHandler<DeleteApartmentCommandRequest, DeleteApartmentCommandResponse>
    {
        private readonly IAparmentRepository apartmentRepository;
        private readonly ICacheService cacheService;

        public DeleteApartmentCommandHandler(IAparmentRepository apartmentRepository, ICacheService cacheService)
        {
            this.apartmentRepository = apartmentRepository;
            this.cacheService = cacheService;
        }

        public async Task<DeleteApartmentCommandResponse> Handle(DeleteApartmentCommandRequest request, CancellationToken cancellationToken)
        {
            var apartment = apartmentRepository.Get(x => x.Id == request.ApartmentId, x => x.User, x => x.Invoices);
            if (apartment == null) throw new NotFoundException("Daire bulunamadı");

            // if no user belong to this apartment and
            // there is no invoices for the apartment
            // we can delete the apartment
            if (apartment.User == null && apartment.Invoices.ToList().Count == 0)
            {
                apartmentRepository.Delete(apartment);
                cacheService.Remove(CacheConstants.ApartmentsKey);
                return new DeleteApartmentCommandResponse { Message = "Daire silindi" };
            }

            throw new BadRequestException("Bu daireyi silemezsiniz");
        }
    }
}
