using Domain.Entities;
using FluentValidation;

namespace Application.Features.Commands.Apartments.CreateApartment
{
    public class CreateApartmentValidator : AbstractValidator<CreateApartmentRequest>
    {
        public CreateApartmentValidator()
        {
            RuleFor(x => x.Floor).GreaterThan(0).WithMessage("floor alanı 0'dan büyük olmalıdır");
        }
    }
}
