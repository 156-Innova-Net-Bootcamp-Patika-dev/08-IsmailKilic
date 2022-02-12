
using FluentValidation;

namespace Application.Features.Commands.Admin.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("email alanı geçerli bir email adresi olmalı");
            RuleFor(x => x.Username).NotEmpty().WithMessage("username alanı boş olmamalı");
            RuleFor(x => x.TCNo).Length(11).WithMessage("tcno alanı 11 karakterden oluşmalıdır");
            RuleFor(x => x.Phone).Length(10).WithMessage("phone alanı 10 karakterden oluşmalıdır");
        }
    }
}
