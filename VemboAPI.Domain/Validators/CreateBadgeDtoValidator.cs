using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateBadgeDtoValidator : AbstractValidator<CreateBadgeDto>
    {
        public CreateBadgeDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.IconUrl).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
