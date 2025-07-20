using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateLessonDtoValidator : AbstractValidator<UpdateLessonDto>
{
    public UpdateLessonDtoValidator()
    {
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
        RuleFor(x => x.LevelId).GreaterThan(0);
    }
}
