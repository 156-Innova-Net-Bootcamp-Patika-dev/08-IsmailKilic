using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using Microsoft.Extensions.Logging;
using PaymentAPI.Data;
using PaymentAPI.Models;

namespace PaymentAPI.Consumers
{
    public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
    {
        private readonly IMongoRepository<Invoice> invoiceRepository;

        public InvoiceCreatedConsumer(IMongoRepository<Invoice> invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task Consume(ConsumeContext<InvoiceCreated> context)
        {
            var createdInvoice = context.Message;
            var invoice = new Invoice
            {
                ApartmentId = createdInvoice.ApartmentId,
                InvoiceId = createdInvoice.InvoiceId,
                InvoiceType = createdInvoice.InvoiceType,
                Month = createdInvoice.Month,
                Year = createdInvoice.Year,
                CreatedAt = System.DateTime.Now
            };

            await invoiceRepository.InsertOneAsync(invoice);
        }
    }
}
