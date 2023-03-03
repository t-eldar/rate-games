namespace RateGames.Models;

public class InvolvedCompany
{
	public int Id { get; set; }
	public Company? Company { get; set; }
	public bool? Developer { get; set; }
	public bool? Porting { get; set; }
	public bool? Publisher { get; set; }
	public bool? Supporting { get; set; }
}