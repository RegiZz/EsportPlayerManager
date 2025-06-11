namespace EsportPlayerMeniger.Models;

public class Training
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public int SkillIncrease { get; set; }
    public int FatigueIncrease { get; set; }
}