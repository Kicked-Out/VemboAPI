using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateLevelTypeDtoValidator : AbstractValidator<CreateLevelTypeDto>
    {
        public CreateLevelTypeDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title must be 50 characters or fewer.");
        }
    }
}
