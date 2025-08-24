namespace Assignment.Api.SuggestTask;
using FluentValidation;

public sealed class SuggestTaskRequestValidator : AbstractValidator<SuggestTaskRequest>
{
    public SuggestTaskRequestValidator()
    {
        RuleFor(req => req.Utterance)
            .Cascade(CascadeMode.Stop) // stop at first failure
            .NotNull().WithMessage("utterance required")
            .NotEmpty().WithMessage("utterance must not be empty");
    }
}