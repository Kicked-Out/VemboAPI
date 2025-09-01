using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserExerciseMistakeDtoValidator : AbstractValidator<CreateUserExerciseMistakeDto>
{
    public CreateUserExerciseMistakeDtoValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.ExerciseId).GreaterThan(0);
        RuleFor(x => x.UserAnswer).NotEmpty();
    }
}
