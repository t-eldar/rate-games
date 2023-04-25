namespace RateGames.Exceptions;

/// <summary>
/// Throw this exception when receive invalid claims.
/// </summary>
public class InvalidClaimsException : Exception
{
	public InvalidClaimsException() : base() { }
	public InvalidClaimsException(string message) : base(message) { }
	public InvalidClaimsException(string message, Exception innerException)
		: base(message, innerException) { }
}
