using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateAchievementLevelDtoValidator : AbstractValidator<CreateAchievementLevelDto>
    {
        public CreateAchievementLevelDtoValidator()
        {
            RuleFor(x => x.AchievementId).GreaterThan(0);
            RuleFor(x => x.Level).InclusiveBetween(1, 10);
            RuleFor(x => x.TargetValue).GreaterThan(0);
            RuleFor(x => x.RewardXP).GreaterThanOrEqualTo(0);
        }
    }
}

