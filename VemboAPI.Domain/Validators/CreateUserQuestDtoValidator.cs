using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserQuestDtoValidator : AbstractValidator<CreateUserQuestDto>
{
    public CreateUserQuestDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.QuestDefinitionId).GreaterThan(0);
    }
}
