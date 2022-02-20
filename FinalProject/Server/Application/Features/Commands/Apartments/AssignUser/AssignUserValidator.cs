using FluentValidation;

namespace Application.Features.Commands.Apartments.AssignUser
{
    public class AssignUserValidator : AbstractValidator<AssignUserRequest>
    {
        public AssignUserValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId alanı boş olmamalıdır");
            RuleFor(x => x.ApartmentId).NotEmpty().WithMessage("ApartmentId alanı boş olmamalıdır");
        }
    }
}
