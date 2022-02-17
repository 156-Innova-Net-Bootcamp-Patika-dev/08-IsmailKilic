using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentAPI.Models;
using PaymentAPI.Models.Dtos;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto dto);
        List<Payment> GetPaymentsByUser(string userId);
    }
}
