using FluentValidation;

namespace Application.Features.Commands.Auth.UpdateUser
{
    internal class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {

            RuleFor(x => x.FullName).NotEmpty().WithMessage("Ad soyad alanı boş olmamalıdır");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon No alanı boş olmamalıdır")
                .Length(10).WithMessage("Telefon No alanı 10 karakterden oluşmaldırı");
            RuleFor(x => x.TCNo).NotEmpty().WithMessage("TC No alanı boş olmamalıdır")
                .Length(11).WithMessage("TC No alanı 11 karakterden oluşmaldırı");
        }
    }
}
