namespace RateGames.Exceptions;

/// <summary>
/// Throw this exception when entity is not found and you cannot return null.
/// </summary>
public class EntityNotFoundException : Exception
{
	public EntityNotFoundException() : base() { }
	public EntityNotFoundException(string message) : base(message) { }
	public EntityNotFoundException(string message, Exception innerException)
		: base(message, innerException) { }
}
