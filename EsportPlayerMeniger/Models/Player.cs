namespace EsportPlayerMeniger.Models;

public class Player
{
    public int Id { get; set; }
    public required string Nickname { get; set; }
    public required string Game { get; set; }
    public int SkillLevel { get; set; } = 30;
    public int StressLevel { get; set; }
    public int FatigueLevel { get; set; }
    public decimal Money { get; set; } = 1000;
    public int RankingPoints { get; set; }
}