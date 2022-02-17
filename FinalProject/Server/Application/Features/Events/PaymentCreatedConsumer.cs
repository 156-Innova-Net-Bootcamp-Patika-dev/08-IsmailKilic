using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using MassTransit;
using MessageContracts.Events;

namespace Application.Features.Events
{
    public class PaymentCreatedConsumer : IConsumer<PaymentCreated>
    {
        private readonly IInvoiceRepository invoiceRepository;

        public PaymentCreatedConsumer(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        public async Task Consume(ConsumeContext<PaymentCreated> context)
        {
            var invoice = invoiceRepository.Get(x => x.Id == context.Message.InvoiceId);
            invoice.IsPaid = true;

            await invoiceRepository.Update(invoice);
        }
    }
}
