using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserLessonProgressDtoValidator : AbstractValidator<CreateUserLessonProgressDto>
{
    public CreateUserLessonProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.LessonId).GreaterThan(0);
    }
}
