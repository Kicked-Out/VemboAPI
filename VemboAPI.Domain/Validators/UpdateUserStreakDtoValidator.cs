using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateUserStreakDtoValidator : AbstractValidator<UpdateUserStreakDto>
    {
        public UpdateUserStreakDtoValidator()
        {
            RuleFor(x => x.CurrentStreak).GreaterThanOrEqualTo(0);
            RuleFor(x => x.LongestStreak).GreaterThanOrEqualTo(0);
            RuleFor(x => x.StreakFreezes).GreaterThanOrEqualTo(0);
        }
    }
}
