using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Commands.Auth.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("OldPassword boş olmamalı")
                .MinimumLength(6).WithMessage("OldPassword en az 6 karakterden oluşmalı");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("NewPassword boş olmamalı")
                .MinimumLength(6).WithMessage("NewPassword en az 6 karakterden oluşmalı");
        }
    }
}
