using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserUnitProgressDtoValidator : AbstractValidator<UpdateUserUnitProgressDto>
{
    public UpdateUserUnitProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.UnitId).GreaterThan(0);
    }
}
