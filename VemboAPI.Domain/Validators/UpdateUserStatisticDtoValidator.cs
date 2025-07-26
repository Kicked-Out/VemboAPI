using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateUserStatisticDtoValidator : AbstractValidator<UpdateUserStatisticDto>
    {
        public UpdateUserStatisticDtoValidator()
        {
            RuleFor(x => x.CurrentPeriodId).GreaterThan(0);
            RuleFor(x => x.Streak).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Emeralds).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Hearts).InclusiveBetween(0, 5);
        }
    }
}

