using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserQuestDtoValidator : AbstractValidator<UpdateUserQuestDto>
{
    public UpdateUserQuestDtoValidator()
    {
        RuleFor(x => x.Progress).GreaterThanOrEqualTo(0);
    }
}
