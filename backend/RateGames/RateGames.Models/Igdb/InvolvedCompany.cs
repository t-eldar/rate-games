using RateGames.Common.Contracts;

namespace RateGames.Models.Igdb;

public class InvolvedCompany : IEntity
{
    public int Id { get; set; }
    public Company? Company { get; set; }
    public bool? Developer { get; set; }
    public bool? Porting { get; set; }
    public bool? Publisher { get; set; }
    public bool? Supporting { get; set; }
}