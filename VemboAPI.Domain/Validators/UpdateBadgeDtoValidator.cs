using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateBadgeDtoValidator : AbstractValidator<UpdateBadgeDto>
    {
        public UpdateBadgeDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.IconUrl).NotEmpty();
        }
    }
}
