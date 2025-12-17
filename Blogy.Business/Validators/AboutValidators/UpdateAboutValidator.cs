using Blogy.Business.DTOs.AboutDtos;
using FluentValidation;

namespace Blogy.Business.Validators.AboutValidators
{
    public class UpdateAboutValidator : AbstractValidator<UpdateAboutDto>
    {
        public UpdateAboutValidator()
        {
            RuleFor(x => x.Title1).NotEmpty().WithMessage("Başlık1 boş bırakılamaz");
            RuleFor(x => x.Description1).NotEmpty().WithMessage("Açıklama1 boş bırakılamaz");
            RuleFor(x => x.ImageUrl1).NotEmpty().WithMessage("Resim1 boş bırakılamaz");
            RuleFor(x => x.Title2).NotEmpty().WithMessage("Başlık2 boş bırakılamaz");
            RuleFor(x => x.Description2).NotEmpty().WithMessage("Açıklama2 boş bırakılamaz");
            RuleFor(x => x.ImageUrl2).NotEmpty().WithMessage("Resim2 boş bırakılamaz");

        }
    }
}
