using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserStatisticDtoValidator : AbstractValidator<CreateUserStatisticDto>
    {
        public CreateUserStatisticDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.CurrentPeriodId).GreaterThan(0);
            RuleFor(x => x.CurrentLevelId).GreaterThan(0);
            RuleFor(x => x.Streak).GreaterThanOrEqualTo(0);
            RuleFor(x => x.VBucks).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Hearts).InclusiveBetween(0, 5);
        }
    }
}

