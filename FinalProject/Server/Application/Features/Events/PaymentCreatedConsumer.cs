using System.Threading.Tasks;
using MassTransit;
using MessageContracts.Events;
using Microsoft.Extensions.Logging;

namespace Application.Features.Events
{
    public class PaymentCreatedConsumer : IConsumer<PaymentCreated>
    {
        ILogger<PaymentCreatedConsumer> _logger;

        public PaymentCreatedConsumer(ILogger<PaymentCreatedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PaymentCreated> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.InvoiceId);
        }
    }
}
