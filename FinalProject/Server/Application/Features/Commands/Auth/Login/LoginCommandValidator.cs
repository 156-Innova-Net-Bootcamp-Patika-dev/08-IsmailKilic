using FluentValidation;

namespace Application.Features.Commands.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("username alanı boş olmamalıdır");
            RuleFor(x => x.Password).NotEmpty().WithMessage("password alanı boş olmamalıdır").
                MinimumLength(6).WithMessage("password alanı en az 6 karakterden oluşmalıdır");
        }
    }
}
