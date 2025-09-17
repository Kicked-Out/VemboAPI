using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class UpdateUserStreakDayDtoValidator : AbstractValidator<UpdateUserStreakDayDto>
    {
        public UpdateUserStreakDayDtoValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.Status).NotEmpty();
        }
    }
}
