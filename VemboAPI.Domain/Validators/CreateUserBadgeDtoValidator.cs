using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserBadgeDtoValidator : AbstractValidator<CreateUserBadgeDto>
    {
        public CreateUserBadgeDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.BadgeId).GreaterThan(0);
            RuleFor(x => x.EarnedAt).NotEmpty();
        }
    }
}
