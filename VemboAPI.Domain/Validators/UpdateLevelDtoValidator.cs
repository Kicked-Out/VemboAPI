using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateLevelDtoValidator : AbstractValidator<UpdateLevelDto>
{
    public UpdateLevelDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.UnitId).GreaterThan(0);
        RuleFor(x => x.Order).GreaterThanOrEqualTo(1);
    }
}