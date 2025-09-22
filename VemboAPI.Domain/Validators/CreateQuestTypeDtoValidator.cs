using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateQuestTypeDtoValidator : AbstractValidator<CreateQuestTypeDto>
{
    public CreateQuestTypeDtoValidator()
    {
        RuleFor(x => x.Type).NotEmpty();
    }
}
