using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateBadgeDtoValidator : AbstractValidator<CreateBadgeDto>
    {
        public CreateBadgeDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.IconUrl).NotEmpty();
        }
    }
}
