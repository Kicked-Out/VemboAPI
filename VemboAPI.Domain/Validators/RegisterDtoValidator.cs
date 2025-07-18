using FluentValidation;

using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);
            RuleFor(x => x.NickName).NotEmpty().MaximumLength(30);
        }
    }
}
