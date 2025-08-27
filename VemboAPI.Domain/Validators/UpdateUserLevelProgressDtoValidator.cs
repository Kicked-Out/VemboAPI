using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserLevelProgressDtoValidator : AbstractValidator<UpdateUserLevelProgressDto>
{
    public UpdateUserLevelProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.LevelId).GreaterThan(0);
    }
}
