using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateUserAchievementDtoValidator : AbstractValidator<UpdateUserAchievementDto>
    {
        public UpdateUserAchievementDtoValidator()
        {
            RuleFor(x => x.CurrentLevel).InclusiveBetween(1, 10);
            RuleFor(x => x.Progress).GreaterThanOrEqualTo(0);
            RuleFor(x => x.EarnedAt).NotEmpty();
        }
    }
}

