using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentAPI.Models.Dtos;
using PaymentAPI.Models.ViewModels;

namespace PaymentAPI.Services
{
    public interface IPaymentService
    {
        Task CreatePayment(CreatePaymentDto dto);
        Task CreatePaymentMany(List<CreatePaymentDto> dto, string userId);
        Task<List<PaymentVM>> GetPaymentsByUser(string userId);
        Task<List<PaymentVM>> GetAllPayments();
    }
}
