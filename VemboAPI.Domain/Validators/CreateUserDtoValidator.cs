using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.NickName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }
    }
}

