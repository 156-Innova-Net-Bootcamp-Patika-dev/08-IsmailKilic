using Application.Features.Commands.Admin.ToggleDelete;
using Application.Features.Commands.Apartments.AssignUser;
using Application.Features.Commands.Apartments.CreateApartment;
using Application.Features.Commands.Apartments.RemoveUser;
using Application.Features.Commands.Auth.UpdateUser;
using Application.Features.Commands.Invoices.CreateInvoice;
using Application.Features.Commands.Invoices.CreateManyInvoices;
using Application.Features.Commands.Messages.SendMessage;
using Application.Features.Queries.GetAllInvoices;
using Application.Features.Queries.GetApartment;
using Application.Features.Queries.GetApartments;
using Application.Features.Queries.GetAuthenticatedUser;
using Application.Features.Queries.GetInvoicesByUser;
using Application.Features.Queries.GetMessages;
using Application.Features.Queries.GetUsers;
using Application.Features.Queries.ReadMessage;
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
            CreateMap<Apartment, GetApartmentResponse>();

            CreateMap<Apartment, AssignUserResponse>();
            CreateMap<Apartment, GetApartmentsResponse>();
            CreateMap<Apartment, RemoveUserResponse>();

            CreateMap<ApplicationUser, UserVM>();
            CreateMap<ApplicationUser, GetUsersResponse>();
            CreateMap<ApplicationUser, UpdateUserResponse>();
            CreateMap<ApplicationUser, GetAuthenticatedUserResponse>();
            CreateMap<ApplicationUser, ToggleDeleteCommandResponse>();

            CreateMap<Apartment, ApartmentVM>();
            
            CreateMap<CreateInvoiceRequest, Invoice>();
            CreateMap<CreateManyInvoicesRequest, Invoice>();
            CreateMap<Invoice, CreateInvoiceResponse>();
            CreateMap<Invoice, GetInvoicesResponse>();
            CreateMap<Invoice, GetAllInvoicesResponse>();
            CreateMap<Invoice, InvoiceVM>();

            CreateMap<Message, SendMessageResponse>();
            CreateMap<Message, GetMessagesResponse>();
            CreateMap<Message, ReadMessageResponse>();
        }
    }
}