namespace EsportPlayerMeniger.Models;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EntryFee { get; set; }
    public decimal PrizePool { get; set; }
    public int MinSkillRequired { get; set; }
}