using FluentValidation;

namespace Application.Features.Commands.Messages.SendMessage
{
    public class SendMessageValidator : AbstractValidator<SendMessageRequest>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x.ReceiverId).NotEmpty().WithMessage("ReceiverId boş olmamalı");
            RuleFor(x => x.SenderId).NotEmpty().WithMessage("SenderId boş olmamalı");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content boş olmamalı");
        }
    }
}
