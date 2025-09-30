using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.Validators
{
    public class CreateUserLeaderBoardEntryDtoValidator : AbstractValidator<CreateUserLeaderBoardEntryDto>
    {
        public CreateUserLeaderBoardEntryDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.TotalXP).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Rank).GreaterThanOrEqualTo(0);
        }
    }
}

