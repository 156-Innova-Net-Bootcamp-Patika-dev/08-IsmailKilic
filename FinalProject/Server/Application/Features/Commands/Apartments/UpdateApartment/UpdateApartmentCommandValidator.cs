using Domain.Entities;
using FluentValidation;

namespace Application.Features.Commands.Apartments.UpdateApartment
{
    public class UpdateApartmentCommandValidator : AbstractValidator<UpdateApartmentCommandRequest>
    {
        public UpdateApartmentCommandValidator()
        {
            RuleFor(x => x.Floor).NotEmpty().WithMessage("Floor alanı boş olmamalıdır")
                .GreaterThan(0).WithMessage("floor alanı 0'dan büyük olmalıdır");
            RuleFor(x => x.OwnerType).IsInEnum();
            RuleFor(x => x.Block).NotEmpty().WithMessage("Block alanı boş olmamalıdır");
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type alanı boş olmamalıdır");
            RuleFor(x => x.No).NotEmpty().WithMessage("No alanı boş olmamalıdır")
                .GreaterThanOrEqualTo(1).WithMessage("No alanı 0'dan büyük olmalıdır");
        }
    }
}
