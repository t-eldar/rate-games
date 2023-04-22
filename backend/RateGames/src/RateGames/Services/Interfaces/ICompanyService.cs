using RateGames.Models.Igdb;

namespace RateGames.Services.Interfaces;

/// <summary>
/// Service for receiving company entities from <see href="igdb.com"/>
/// </summary>
public interface ICompanyService
{
	Task<Company?> GetByIdAsync(int id);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="code">ISO 3166-1 country code</param>
	/// <returns></returns>
	Task<IEnumerable<Company>?> GetAllByCountryAsync(int code);
}
