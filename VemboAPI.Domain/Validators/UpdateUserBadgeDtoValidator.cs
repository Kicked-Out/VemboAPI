using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateUserBadgeDtoValidator : AbstractValidator<UpdateUserBadgeDto>
    {
        public UpdateUserBadgeDtoValidator()
        {
            RuleFor(x => x.EarnedAt).NotEmpty();
        }
    }
}
