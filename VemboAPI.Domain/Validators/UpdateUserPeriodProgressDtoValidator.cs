using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserPeriodProgressDtoValidator : AbstractValidator<UpdateUserPeriodProgressDto>
{
    public UpdateUserPeriodProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.PeriodId).GreaterThan(0);
    }
}
