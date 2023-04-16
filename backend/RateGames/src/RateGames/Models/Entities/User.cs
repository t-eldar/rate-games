using Microsoft.AspNetCore.Identity;

namespace RateGames.Models.Entities;

public class User : IdentityUser
{
	public string? AvatarUrl { get; set; } 
}
