using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUnitDtoValidator : AbstractValidator<CreateUnitDto>
{
    public CreateUnitDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(250);
        RuleFor(x => x.Order).GreaterThanOrEqualTo(1);
        RuleFor(x => x.TopicId).GreaterThan(0);
    }
}