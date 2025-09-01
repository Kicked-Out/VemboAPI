using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserQuestDtoValidator : AbstractValidator<CreateUserQuestDto>
{
    public CreateUserQuestDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.QuestId).GreaterThan(0);
    }
}
