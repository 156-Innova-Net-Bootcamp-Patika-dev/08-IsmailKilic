using System.Threading.Tasks;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task CreatePayment(int value);
    }
}
