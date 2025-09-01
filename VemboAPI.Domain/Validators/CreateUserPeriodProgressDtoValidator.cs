using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserPeriodProgressDtoValidator : AbstractValidator<CreateUserPeriodProgressDto>
{
    public CreateUserPeriodProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.PeriodId).GreaterThan(0);
    }
}
