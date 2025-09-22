using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserQuestProgressDtoValidator : AbstractValidator<CreateUserQuestProgressDto>
{
    public CreateUserQuestProgressDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.QuestId).GreaterThan(0);
        RuleFor(x => x.Progress).GreaterThanOrEqualTo(0);
    }
}
