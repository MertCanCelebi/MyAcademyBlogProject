using Blogy.Entity.Entities;
using FluentValidation;

namespace Blogy.Business.Validators.MessageValidators
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu boş olamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail boş olamaz.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik boş olamaz.");

        }
    }
}
