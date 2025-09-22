using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateAchievementDtoValidator : AbstractValidator<UpdateAchievementDto>
    {
        public UpdateAchievementDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
            RuleFor(x => x.IconUrl).MaximumLength(300);
            RuleFor(x => x.CompletedIconUrl).MaximumLength(300);
        }
    }
}
