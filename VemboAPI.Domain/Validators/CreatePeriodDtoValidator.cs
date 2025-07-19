using FluentValidation;

using VemboAPI.Domain.DTOs;

public class CreatePeriodDtoValidator : AbstractValidator<CreatePeriodDto>
{
    public CreatePeriodDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("ImageUrl is required.")
            .MaximumLength(500);
    }
}
