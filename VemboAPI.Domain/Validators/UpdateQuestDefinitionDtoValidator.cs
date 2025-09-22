using System;
using FluentValidation;
using VemboAPI.Domain.DTOs;
using VemboAPI.Domain.Entities;

public class UpdateQuestDefinitionDtoValidator : AbstractValidator<UpdateQuestDefinitionDto>
{
    public UpdateQuestDefinitionDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Category)
            .NotEmpty()
            .Must(value => Enum.TryParse<QuestDefinitionCategory>(value, true, out _))
            .WithMessage($"Category must be one of: {string.Join(", ", Enum.GetNames(typeof(QuestDefinitionCategory)))}");
        RuleFor(x => x.Requirement).GreaterThan(0);
        RuleFor(x => x.RewardType).NotEmpty();
        RuleFor(x => x.RewardAmount).GreaterThanOrEqualTo(0);
    }
}
