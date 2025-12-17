using Blogy.Entity.Entities;
using FluentValidation;

namespace Blogy.Business.Validators.SocialValidators
{
    public class SocialValidator : AbstractValidator<Social>
    {
        public SocialValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz.");
            RuleFor(x => x.Icon).NotEmpty().WithMessage("İkon boş olamaz.");
            RuleFor(x => x.Url).NotEmpty().WithMessage("Url boş olamaz.");
        }
    }
}
