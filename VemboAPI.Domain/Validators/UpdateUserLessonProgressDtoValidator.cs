using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserLessonProgressDtoValidator : AbstractValidator<UpdateUserLessonProgressDto>
{
    public UpdateUserLessonProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.LessonId).GreaterThan(0);
    }
}
