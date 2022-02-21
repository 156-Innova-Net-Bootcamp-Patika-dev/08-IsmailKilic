using AutoMapper;
using PaymentAPI.Models;
using PaymentAPI.Models.ViewModels;

namespace PaymentAPI.Settings
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Payment, PaymentVM>();
        }
    }
}
