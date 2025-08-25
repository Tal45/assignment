namespace Assignment.Api.SuggestTask;
using FluentValidation;

public sealed class SuggestTaskRequestValidator : AbstractValidator<SuggestTaskRequest>
{
    public SuggestTaskRequestValidator()
    {
        RuleFor(req => req.Utterance).NotEmpty().WithMessage("utterance required");
    }
}