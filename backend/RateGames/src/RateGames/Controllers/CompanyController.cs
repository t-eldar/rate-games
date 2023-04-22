using Microsoft.AspNetCore.Mvc;

using RateGames.Services.Interfaces;

namespace RateGames.Controllers;

/// <summary>
/// Controller for getting companies from <see href="igdb.com"/>
/// </summary>
[Route("companies")]
[ApiController]
public class CompanyController : ControllerBase
{
	private readonly ICompanyService _companyService;

	public CompanyController(ICompanyService companyService) => _companyService = companyService;

	[Route("{id}")]
	[HttpGet]
	public async Task<IActionResult> GetByIdAsync(int id)
	{
		var company = await _companyService.GetByIdAsync(id);

		return company is null 
			? NotFound()
			: Ok(company);
	}

	[Route("by-country")]
	[HttpGet]
	public async Task<IActionResult> GetAllByCountryAsync(int countryCode)
	{
		var company = await _companyService.GetAllByCountryAsync(countryCode);

		return company is null
			? NotFound()
			: Ok(company);
	}
}

