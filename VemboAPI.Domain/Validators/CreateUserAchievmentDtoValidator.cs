using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserAchievementDtoValidator : AbstractValidator<CreateUserAchievementDto>
    {
        public CreateUserAchievementDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.AchievementId).GreaterThan(0);
            RuleFor(x => x.CurrentLevel).InclusiveBetween(1, 10);
            RuleFor(x => x.Progress).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EarnedAt).NotEmpty();
        }
    }
}

