using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateQuestionDtoValidator : AbstractValidator<UpdateQuestionDto>
{
    public UpdateQuestionDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
        RuleFor(x => x.ExerciseId).GreaterThan(0);
    }
}
