using FluentValidation;
using VemboAPI.Domain.DTOs;

public class CreateUserTopicProgressDtoValidator : AbstractValidator<CreateUserTopicProgressDto>
{
    public CreateUserTopicProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.TopicId).GreaterThan(0);
    }
}
