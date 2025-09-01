using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserUnitProgressDtoValidator : AbstractValidator<UpdateUserUnitProgressDto>
{
    public UpdateUserUnitProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.UnitId).GreaterThan(0);
    }
}
