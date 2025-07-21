using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserLevelProgressDtoValidator : AbstractValidator<CreateUserLevelProgressDto>
{
    public CreateUserLevelProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.LevelId).GreaterThan(0);
    }
}
