using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateAnswerDtoValidator : AbstractValidator<CreateAnswerDto>
{
    public CreateAnswerDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
        RuleFor(x => x.QuestionId).GreaterThan(0);
    }
}
