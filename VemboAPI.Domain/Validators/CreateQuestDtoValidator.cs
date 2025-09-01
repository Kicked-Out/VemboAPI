using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateQuestDtoValidator : AbstractValidator<CreateQuestDto>
{
    public CreateQuestDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.QuestType).NotEmpty();
        RuleFor(x => x.Requirement).GreaterThan(0);
        RuleFor(x => x.RewardType).NotEmpty().MaximumLength(50);
        RuleFor(x => x.RewardAmount).GreaterThan(0);
    }
}
