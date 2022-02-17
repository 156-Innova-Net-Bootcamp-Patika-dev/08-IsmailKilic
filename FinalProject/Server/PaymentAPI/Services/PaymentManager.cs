using System;
using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using PaymentAPI.Data;
using PaymentAPI.Models;
using PaymentAPI.Models.Dtos;

namespace PaymentAPI.Services
{
    public class PaymentManager : IPaymentService
    {
        private readonly IMongoRepository<Payment> paymentRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMongoRepository<User> userRepository;
        private readonly IMongoRepository<Invoice> invoiceRepository;

        public PaymentManager(IMongoRepository<Payment> paymentRepository, IPublishEndpoint publishEndpoint,
            IMongoRepository<User> userRepository, IMongoRepository<Invoice> invoiceRepository)
        {
            this.paymentRepository = paymentRepository;
            this.publishEndpoint = publishEndpoint;
            this.userRepository = userRepository;
            this.invoiceRepository = invoiceRepository;
        }

        public async Task CreatePayment(CreatePaymentDto dto)
        {
            // check if invoice paid before
            var paymentExisted = await paymentRepository.FindOneAsync(x => x.InvoiceId == dto.InvoiceId);
            if (paymentExisted != null) throw new Exception("Bu fatura daha önce ödenmiş");

            // check if user existed
            var user = await userRepository.FindOneAsync(x => x.UserId == dto.UserId);
            if (user == null) throw new Exception("Kullanıcı bulunamadı");

            // check if user balance is greater than invoice price
            if (user.Balance < dto.Price)
            {
                user.Balance += 1000;
                await userRepository.ReplaceOneAsync(user);
                throw new Exception("Yeterli bakiye bulunamadı. Bakiyeniz 1000 TL yükseltildi");
            }

            // check if invoice existed
            var invoice = await invoiceRepository.FindOneAsync(x => x.InvoiceId == dto.InvoiceId && x.ApartmentId == dto.ApartmentId);
            if (invoice == null) throw new Exception("Fatura bulunamadı");

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
    }
}
