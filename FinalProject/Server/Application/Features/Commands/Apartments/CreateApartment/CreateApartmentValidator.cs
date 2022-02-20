using Domain.Entities;
using FluentValidation;

namespace Application.Features.Commands.Apartments.CreateApartment
{
    public class CreateApartmentValidator : AbstractValidator<CreateApartmentRequest>
    {
        public CreateApartmentValidator()
        {
            RuleFor(x => x.Floor).NotEmpty().WithMessage("Floor alanı boş olmamalıdır")
                .GreaterThan(0).WithMessage("floor alanı 0'dan büyük olmalıdır");
            RuleFor(x => x.Block).NotEmpty().WithMessage("Block alanı boş olmamalıdır");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type alanı boş olmamalıdır");
            RuleFor(x => x.No).NotEmpty().WithMessage("No alanı boş olmamalıdır")
                .GreaterThanOrEqualTo(1).WithMessage("No alanı 0'dan büyük olmalıdır");
        }
    }
}
