using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateBadgeDtoValidator : AbstractValidator<UpdateBadgeDto>
    {
        public UpdateBadgeDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.IconUrl).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
