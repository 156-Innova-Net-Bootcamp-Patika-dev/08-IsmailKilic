using FluentValidation;

namespace Application.Features.Commands.Apartments.RemoveUser
{
    public class RemoveUserValidator : AbstractValidator<RemoveUserRequest>
    {
        public RemoveUserValidator()
        {
            RuleFor(x => x.ApartmentId).NotEmpty().WithMessage("ApartmentId boş olmamalıdır");
        }
    }
}
