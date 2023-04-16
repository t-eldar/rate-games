namespace Apicalypse.Tests.TestModels;
public class Person
{
    public int Id { get; set; }
    public Country Country { get; set; } = null!;
    public int[] Array { get; set; } = null!;
}
