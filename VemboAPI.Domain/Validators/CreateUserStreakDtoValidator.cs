using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserStreakDtoValidator : AbstractValidator<CreateUserStreakDto>
    {
        public CreateUserStreakDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.CurrentStreak).GreaterThanOrEqualTo(0);
            RuleFor(x => x.LongestStreak).GreaterThanOrEqualTo(0);
            RuleFor(x => x.StreakFreezes).GreaterThanOrEqualTo(0);
        }
    }
}
