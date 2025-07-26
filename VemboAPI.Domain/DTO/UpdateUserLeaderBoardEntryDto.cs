using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;

namespace VemboAPI.Domain.DTOs
{
    public class UpdateUserLeaderBoardEntryDtoValidator : AbstractValidator<UpdateUserLeaderBoardEntryDto>
    {
        public UpdateUserLeaderBoardEntryDtoValidator()
        {
            RuleFor(x => x.XP).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Rank).GreaterThanOrEqualTo(0);
        }
    }
}

