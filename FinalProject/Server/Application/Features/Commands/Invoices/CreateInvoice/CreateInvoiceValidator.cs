using FluentValidation;

namespace Application.Features.Commands.Invoices.CreateInvoice
{
    public class CreateInvoiceValidator : AbstractValidator<CreateInvoiceRequest>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.InvoiceType).IsInEnum();
            RuleFor(x => x.Month).NotEmpty().WithMessage("Month alanı boş olmamalı")
                .GreaterThanOrEqualTo(1).WithMessage("Month büyük eşittir 1 olmalı")
                .LessThanOrEqualTo(12).WithMessage("Month küçük eşittir 12 olmalı");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Year alanı boş olmamalı")
                .GreaterThanOrEqualTo(1990).WithMessage("Year büyük eşittir 1990 olmalı");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price boş olmamalı")
                .GreaterThan(0).WithMessage("Price 0'dan büyük olmalı");
            RuleFor(x => x.ApartmentId).NotEmpty().WithMessage("ApartmentId boş olmamalı");
        }
    }
}
