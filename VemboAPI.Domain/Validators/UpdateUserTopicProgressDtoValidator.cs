using FluentValidation;
using VemboAPI.Domain.DTOs;

public class UpdateUserTopicProgressDtoValidator : AbstractValidator<UpdateUserTopicProgressDto>
{
    public UpdateUserTopicProgressDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.TopicId).GreaterThan(0);
    }
}
