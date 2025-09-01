using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserUnitProgressDtoValidator : AbstractValidator<CreateUserUnitProgressDto>
{
    public CreateUserUnitProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.UnitId).GreaterThan(0);
    }
}
