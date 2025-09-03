using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserStreakDayDtoValidator : AbstractValidator<CreateUserStreakDayDto>
    {
        public CreateUserStreakDayDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
