using Blogy.Entity.Entities;
using FluentValidation;

namespace Blogy.Business.Validators.TagValidators
{
    public class TagValidator : AbstractValidator<Tag>
    {
        public TagValidator() 
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz.");
        }
    }
}
