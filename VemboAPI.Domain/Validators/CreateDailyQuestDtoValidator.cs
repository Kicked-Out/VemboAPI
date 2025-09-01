using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateDailyQuestDtoValidator : AbstractValidator<CreateDailyQuestDto>
{
    public CreateDailyQuestDtoValidator()
    {
        RuleFor(x => x.QuestId).GreaterThan(0);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
    }
}
