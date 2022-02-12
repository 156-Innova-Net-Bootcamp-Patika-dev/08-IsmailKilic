using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Features.Commands.Apartments.RemoveUser;
using Application.Features.Queries.GetApartments;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<CreateApartmentRequest, Apartment>();
            CreateMap<Apartment, CreateApartmentResponse>();

            CreateMap<Apartment, AssignUserResponse>();
            CreateMap<Apartment, GetApartmentsResponse>();
            CreateMap<Apartment, RemoveUserResponse>();
            
        }
    }
}