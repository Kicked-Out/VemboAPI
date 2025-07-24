using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateGuideBookDtoValidator : AbstractValidator<CreateGuideBookDto>
    {
        public CreateGuideBookDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
