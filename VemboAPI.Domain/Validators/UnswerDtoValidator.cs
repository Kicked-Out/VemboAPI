using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateAnswerDtoValidator : AbstractValidator<UpdateAnswerDto>
{
    public UpdateAnswerDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
        RuleFor(x => x.QuestionId).GreaterThan(0);
    }
}
