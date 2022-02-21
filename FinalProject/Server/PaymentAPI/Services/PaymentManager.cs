using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using AutoMapper;
using MassTransit;
using MessageContracts.Events;
using MongoDB.Driver;
using PaymentAPI.Data;
using PaymentAPI.Models;
using PaymentAPI.Models.Dtos;
using PaymentAPI.Models.ViewModels;

namespace PaymentAPI.Services
{
    public class PaymentManager : IPaymentService
    {
        private readonly IMongoRepository<Payment> paymentRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMongoRepository<User> userRepository;
        private readonly IMongoRepository<Invoice> invoiceRepository;
        private readonly IMapper mapper;

        public PaymentManager(IMongoRepository<Payment> paymentRepository, IPublishEndpoint publishEndpoint,
            IMongoRepository<User> userRepository, IMongoRepository<Invoice> invoiceRepository, IMapper mapper)
        {
            this.paymentRepository = paymentRepository;
            this.publishEndpoint = publishEndpoint;
            this.userRepository = userRepository;
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        public async Task CreatePayment(CreatePaymentDto dto)
        {
            // check if invoice paid before
            var paymentExisted = await paymentRepository.FindOneAsync(x => x.InvoiceId == dto.InvoiceId);
            if (paymentExisted != null) throw new BadRequestException("Bu fatura daha önce ödenmiş");

            // check if user existed
            var user = await userRepository.FindOneAsync(x => x.UserId == dto.UserId);
            if (user == null) throw new BadRequestException("Kullanıcı bulunamadı");

            // check if user balance is greater than invoice price
            if (user.Balance < dto.Price)
            {
                user.Balance += dto.Price;
                await userRepository.ReplaceOneAsync(user);
                throw new BadRequestException($"Yeterli bakiye bulunamadı. Bakiyeniz {dto.Price} TL yükseltildi");
            }

            // check if invoice existed
            var invoice = await invoiceRepository.FindOneAsync(x => x.InvoiceId == dto.InvoiceId && x.ApartmentId == dto.ApartmentId);
            if (invoice == null) throw new BadRequestException("Fatura bulunamadı");

            var payment = new Payment
            {
                ApartmentId = dto.ApartmentId,
                InvoiceId = dto.InvoiceId,
                Price = dto.Price,
                UserId = dto.UserId,
                Last4Number = dto.Last4Number,
                CreatedAt = DateTime.Now
            };

            // publish payment-created event
            _ = publishEndpoint.Publish(new PaymentCreated
            {
                InvoiceId = dto.InvoiceId
            });

            user.Balance -= dto.Price;
            await userRepository.ReplaceOneAsync(user);
            await paymentRepository.InsertOneAsync(payment);
        }

        public async Task<List<PaymentVM>> GetPaymentsByUser(string userId)
        {
            var payments = paymentRepository.FilterBy(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToList();
            var data = mapper.Map<List<PaymentVM>>(payments);

            for (int i = 0; i < payments.Count; i++)
            {
                var user = await userRepository.FindOneAsync(x=>x.UserId == payments[i].UserId);
                data[i].User = user;

                var invoice = await invoiceRepository.FindOneAsync(x=> x.InvoiceId == payments[i].InvoiceId);
                data[i].Invoice = invoice;
            }

            return data;
        }

        public async Task<List<PaymentVM>> GetAllPayments()
        {
            var payments = paymentRepository.AsQueryable().OrderByDescending(x => x.CreatedAt).ToList();
            var data = mapper.Map<List<PaymentVM>>(payments);

            for (int i = 0; i < payments.Count; i++)
            {
                var user = await userRepository.FindOneAsync(x => x.UserId == payments[i].UserId);
                data[i].User = user;

                var invoice = await invoiceRepository.FindOneAsync(x => x.InvoiceId == payments[i].InvoiceId);
                data[i].Invoice = invoice;
            }

            return data;
        }

        public async Task CreatePaymentMany(List<CreatePaymentDto> dto, string userId)
        {
            double totalPrice = 0;
            foreach (var item in dto) totalPrice += item.Price;

            // check if user existed
            var user = await userRepository.FindOneAsync(x => x.UserId == userId);
            if (user == null) throw new BadRequestException("Kullanıcı bulunamadı");

            // check if user balance is greater than invoice price
            if (user.Balance < totalPrice)
            {
                user.Balance += totalPrice;
                await userRepository.ReplaceOneAsync(user);
                throw new BadRequestException($"Yeterli bakiye bulunamadı. Bakiyeniz {totalPrice} TL yükseltildi");
            }

            foreach (var item in dto)
            {
                // check if invoice paid before
                var paymentExisted = await paymentRepository.FindOneAsync(x => x.InvoiceId == item.InvoiceId);
                if (paymentExisted != null) continue;

                // check if invoice existed
                var invoice = await invoiceRepository.FindOneAsync(x => x.InvoiceId == item.InvoiceId && x.ApartmentId == item.ApartmentId);
                if (invoice == null) continue;

                var payment = new Payment
                {
                    ApartmentId = item.ApartmentId,
                    InvoiceId = item.InvoiceId,
                    Price = item.Price,
                    UserId = userId,
                    Last4Number = item.Last4Number,
                    CreatedAt = DateTime.Now
                };

                // publish payment-created event
                _ = publishEndpoint.Publish(new PaymentCreated
                {
                    InvoiceId = item.InvoiceId
                });

                user.Balance -= item.Price;
                await userRepository.ReplaceOneAsync(user);
                await paymentRepository.InsertOneAsync(payment);
            }
        }
    }
}
