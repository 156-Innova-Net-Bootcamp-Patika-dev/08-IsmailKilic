using FluentValidation;

namespace Application.Features.Commands.Admin.ToggleDelete
{
    public class ToggleDeleteCommandValidator : AbstractValidator<ToggleDeleteCommandRequest>
    {
        public ToggleDeleteCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId alanı boş olmamalı");
        }
    }
}
