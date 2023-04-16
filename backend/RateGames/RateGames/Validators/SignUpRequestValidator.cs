using FluentValidation;

using RateGames.Models.Requests;

namespace RateGames.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(r => r.Email).NotEmpty().NotNull().EmailAddress();
    }
}
