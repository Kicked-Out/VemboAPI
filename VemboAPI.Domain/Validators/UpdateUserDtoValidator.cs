using FluentValidation;
using VemboAPI.Domain.DTOs;
namespace VemboAPI.Domain.Validators
{
    

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.NickName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }

}

