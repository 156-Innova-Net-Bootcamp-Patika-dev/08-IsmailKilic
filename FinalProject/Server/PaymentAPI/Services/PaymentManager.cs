using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using PaymentAPI.Data;
using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    public class PaymentManager : IPaymentService
    {
        private readonly IMongoRepository<Payment> paymentRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public PaymentManager(IMongoRepository<Payment> paymentRepository, IPublishEndpoint publishEndpoint)
        {
            this.paymentRepository = paymentRepository;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task CreatePayment(int value)
        {
            _ = publishEndpoint.Publish(new PaymentCreated
            {
                InvoiceId = value
            });
        }
    }
}
