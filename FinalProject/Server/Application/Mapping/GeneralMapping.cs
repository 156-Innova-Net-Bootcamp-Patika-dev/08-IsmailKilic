using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Features.Commands.Apartments.RemoveUser;
using Application.Features.Commands.Invoices.CreateInvoice;
using Application.Features.Commands.Messages.SendMessage;
using Application.Features.Queries.GetApartments;
using Application.Features.Queries.GetInvoices;
using AutoMapper;
using Domain.Entities;
using Domain.ViewModels;

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

            CreateMap<ApplicationUser, UserVM>();
            CreateMap<Apartment, ApartmentVM>();
            
            CreateMap<CreateInvoiceRequest, Invoice>();
            CreateMap<Invoice, CreateInvoiceResponse>();
            CreateMap<Invoice, GetInvoicesResponse>();

            CreateMap<Message, SendMessageResponse>();
        }
    }
}