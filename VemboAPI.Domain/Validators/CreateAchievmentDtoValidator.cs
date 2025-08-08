using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateAchievementDtoValidator : AbstractValidator<CreateAchievementDto>
    {
        public CreateAchievementDtoValidator()
        {
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
            RuleFor(x => x.IconUrl).NotEmpty().MaximumLength(300);
        }
    }

}

