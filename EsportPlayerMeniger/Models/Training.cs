namespace EsportPlayerMeniger.Models;

public class Training
{
    public int Id { get; set; }
    public required string Type { get; set; } // np. "Aim Training", "Analiza Taktyki"
    public int SkillIncrease { get; set; }
    public int FatigueIncrease { get; set; }
    public int StressIncrease { get; set; }
    public decimal Cost { get; set; } = 50;
}