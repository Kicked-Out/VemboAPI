using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateExerciseTypeDtoValidator : AbstractValidator<UpdateExerciseTypeDto>
{
    public UpdateExerciseTypeDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100);
    }
}
