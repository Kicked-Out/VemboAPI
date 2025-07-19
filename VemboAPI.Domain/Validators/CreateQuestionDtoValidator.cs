using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateQuestionDtoValidator : AbstractValidator<CreateQuestionDto>
{
    public CreateQuestionDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
        RuleFor(x => x.ExerciseId).GreaterThan(0);
    }
}
