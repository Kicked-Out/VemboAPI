using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserLessonProgressDtoValidator : AbstractValidator<UpdateUserLessonProgressDto>
{
    public UpdateUserLessonProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.LessonId).GreaterThan(0);
    }
}
