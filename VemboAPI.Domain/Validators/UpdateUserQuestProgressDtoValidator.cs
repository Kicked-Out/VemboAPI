using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserQuestProgressDtoValidator : AbstractValidator<UpdateUserQuestProgressDto>
{
    public UpdateUserQuestProgressDtoValidator()
    {
        RuleFor(x => x.Progress).GreaterThanOrEqualTo(0);
    }
}
