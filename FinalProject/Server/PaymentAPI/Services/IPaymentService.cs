using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentAPI.Models;
using PaymentAPI.Models.Dtos;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto dto);
        Task CreatePaymentMany(List<CreatePaymentDto> dto, string userId);
        List<Payment> GetPaymentsByUser(string userId);
        List<Payment> GetAllPayments();
    }
}
