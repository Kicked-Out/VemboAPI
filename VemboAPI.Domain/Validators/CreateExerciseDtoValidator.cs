using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateExerciseDtoValidator : AbstractValidator<CreateExerciseDto>
{
    public CreateExerciseDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.ExerciseTypeId).GreaterThan(0);
        RuleFor(x => x.Order).GreaterThanOrEqualTo(0);
    }
}
