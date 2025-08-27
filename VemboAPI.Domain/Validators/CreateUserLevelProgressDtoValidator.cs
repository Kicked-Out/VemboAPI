using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserLevelProgressDtoValidator : AbstractValidator<CreateUserLevelProgressDto>
{
    public CreateUserLevelProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.LevelId).GreaterThan(0);
    }
}
