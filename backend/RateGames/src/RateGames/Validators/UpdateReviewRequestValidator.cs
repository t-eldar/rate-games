using FluentValidation;

using RateGames.Models.Requests;

namespace RateGames.Validators;

public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
{
	public UpdateReviewRequestValidator()
	{
		RuleFor(review => review.Id).NotEmpty().GreaterThan(0);

		RuleFor(review => review.Title)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3)
			.MaximumLength(100)
			.When(review => review.Title is not null);

		RuleFor(review => review.Description)
			.NotNull()
			.NotEmpty()
			.MinimumLength(3)
			.When(review => review.Title is not null);

		RuleFor(review => review.RatingId)
			.NotEmpty()
			.GreaterThan(0)
			.When(review => review.RatingId is not null);
	}
}
