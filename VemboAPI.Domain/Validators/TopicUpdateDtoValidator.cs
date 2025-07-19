using FluentValidation;
using VemboAPI.Domain.DTOs;

public class TopicUpdateDtoValidator : AbstractValidator<TopicUpdateDto>
{
    public TopicUpdateDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(350);
        RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(300);
        RuleFor(x => x.PeriodId).GreaterThan(0);
    }
}