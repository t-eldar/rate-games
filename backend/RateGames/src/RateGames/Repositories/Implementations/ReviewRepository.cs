using FluentValidation;

using Microsoft.EntityFrameworkCore;

using RateGames.DatabaseContext;
using RateGames.Exceptions;
using RateGames.Models.Entities;
using RateGames.Models.Requests;
using RateGames.Repositories.Interfaces;
using RateGames.Services.Interfaces;

namespace RateGames.Repositories.Implementations;

/// <inheritdoc cref="IReviewRepository"/>
public class ReviewRepository : IReviewRepository
{
	private readonly IApplicationContext _applicationContext;
	private readonly IRatingRepository _ratingRepository;
	private readonly IDateTimeProvider _dateTimeProvider;
	private readonly IValidator<CreateReviewRequest> _createRequestValidator;
	private readonly IValidator<UpdateReviewRequest> _updateRequestValidator;

	public ReviewRepository(
		IApplicationContext applicationContext,
		IDateTimeProvider dateTimeProvider,
		IValidator<CreateReviewRequest> createRequestValidator,
		IValidator<UpdateReviewRequest> updateRequestValidator,
		IRatingRepository ratingRepository
	)
	{
		_applicationContext = applicationContext;
		_createRequestValidator = createRequestValidator;
		_dateTimeProvider = dateTimeProvider;
		_updateRequestValidator = updateRequestValidator;
		_ratingRepository = ratingRepository;
	}

	public async Task<Review> CreateAsync(CreateReviewRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		var validationResult = await _createRequestValidator.ValidateAsync(request);
		if (!validationResult.IsValid)
		{
			throw new ValidationException("Request is invalid");
		}

		var review = request.ToReview(_dateTimeProvider.CurrentUtc);
		await _applicationContext.Reviews.AddRangeAsync(review);
		await _applicationContext.SaveChangesAsync();

		return review;
	}

	public async Task UpdateAsync(UpdateReviewRequest request)
	{
		ArgumentNullException.ThrowIfNull(request);

		var validationResult = await _updateRequestValidator.ValidateAsync(request);
		if (!validationResult.IsValid)
		{
			throw new ValidationException("Request is invalid");
		}

		var review = await _applicationContext.Reviews
			.FirstOrDefaultAsync(r => r.Id == request.Id)
			?? throw new EntityNotFoundException();

		var rating = request.RatingValue is null
			? null 
			: await _ratingRepository.GetByValueAsync(request.RatingValue.Value);

		request.UpdateReview(review, rating?.Id);
		await _applicationContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		var review = await _applicationContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
		if (review is null)
		{
			return;
		}
		_applicationContext.Reviews.Remove(review);
		await _applicationContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<Review>?> GetAllAsync(int limit, int offset) => 
		await _applicationContext.Reviews
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

	public async Task<IEnumerable<Review>?> GetByGameAsync(int gameId,int limit,int offset) => 
		await _applicationContext.Reviews
			.Where(r => r.GameId == gameId)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();


	public async Task<IEnumerable<Review>?> GetByUserAsync(string userId, int limit, int offset) => 
		await _applicationContext.Reviews
			.Where(r => r.UserId == userId)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();


	public async Task<Review?> GetByIdAsync(int id) => 
		await _applicationContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
}
