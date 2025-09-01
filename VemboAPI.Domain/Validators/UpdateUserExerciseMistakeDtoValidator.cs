using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserExerciseMistakeDtoValidator : AbstractValidator<UpdateUserExerciseMistakeDto>
{
    public UpdateUserExerciseMistakeDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.ExerciseId).GreaterThan(0);
        RuleFor(x => x.UserAnswer).NotEmpty();
    }
}
