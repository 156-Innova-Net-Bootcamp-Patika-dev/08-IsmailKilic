using System.Threading.Tasks;
using PaymentAPI.Models.Dtos;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto dto);
    }
}
