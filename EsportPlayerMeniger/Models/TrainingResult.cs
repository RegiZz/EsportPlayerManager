namespace EsportPlayerMeniger.Models;

public class TrainingResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public int SkillGained { get; set; }
    public int FatigueIncreasedBy { get; set; }
    public int StressIncreasedBy { get; set; }
}