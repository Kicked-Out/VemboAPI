using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateQuestTypeDtoValidator : AbstractValidator<UpdateQuestTypeDto>
{
    public UpdateQuestTypeDtoValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
    }
}
