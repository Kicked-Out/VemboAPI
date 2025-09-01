using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateDailyQuestDtoValidator : AbstractValidator<UpdateDailyQuestDto>
{
    public UpdateDailyQuestDtoValidator()
    {
        RuleFor(x => x.QuestId).GreaterThan(0);
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate);
    }
}
