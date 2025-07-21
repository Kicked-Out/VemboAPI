using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateExerciseTypeDtoValidator : AbstractValidator<CreateExerciseTypeDto>
{
    public CreateExerciseTypeDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100);
    }
}
