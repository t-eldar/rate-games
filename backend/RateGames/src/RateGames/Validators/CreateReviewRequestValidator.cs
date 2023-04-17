using FluentValidation;

using RateGames.Models.Requests;

namespace RateGames.Validators;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
	public CreateReviewRequestValidator()
	{
		RuleFor(review => review.Title)
			.NotEmpty()
			.NotNull()
			.MinimumLength(3)
			.MaximumLength(100);

		RuleFor(review => review.Description)
			.NotEmpty()
			.NotNull()
			.MinimumLength(3);

		RuleFor(review => review.RatingId)
			.NotEmpty()
			.GreaterThan(0);
	}
}
