using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserLevelProgressDtoValidator : AbstractValidator<UpdateUserLevelProgressDto>
{
    public UpdateUserLevelProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.LevelId).GreaterThan(0);
    }
}
