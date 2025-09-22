using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateQuestDtoValidator : AbstractValidator<UpdateQuestDto>
{
    public UpdateQuestDtoValidator()
    {
        RuleFor(x => x.QuestDefinitionId).GreaterThan(0);
        RuleFor(x => x.QuestTypeId).GreaterThan(0);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
    }
}
