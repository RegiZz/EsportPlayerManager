using System.Collections.Generic;
using EsportPlayerMeniger.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EsportPlayerMeniger.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!await context.Tournaments.AnyAsync())
        {
            var tournaments = new List<Tournament>
            {
                new() { Name = "Turniej Początkujących", EntryFee = 50, PrizePool = 500, MinSkillRequired = 20 },
                new() { Name = "Liga Amatorska", EntryFee = 100, PrizePool = 1000, MinSkillRequired = 40 },
                new() { Name = "Mistrzostwa Regionalne", EntryFee = 200, PrizePool = 2500, MinSkillRequired = 60 },
                new() { Name = "Turniej Pro", EntryFee = 500, PrizePool = 5000, MinSkillRequired = 80 },
                new() { Name = "Mistrzostwa Świata", EntryFee = 1000, PrizePool = 15000, MinSkillRequired = 90 }
            };
            await context.Tournaments.AddRangeAsync(tournaments);
        }

        if (!await context.Trainings.AnyAsync())
        {
            var trainings = new List<Training>
            {
                new() { Type = "Podstawy Gry", SkillIncrease = 5, FatigueIncrease = 10 },
                new() { Type = "Trening Refleksu", SkillIncrease = 3, FatigueIncrease = 15 },
                new() { Type = "Analiza Map", SkillIncrease = 4, FatigueIncrease = 8 },
                new() { Type = "Trening Zespołowy", SkillIncrease = 6, FatigueIncrease = 12 },
                new() { Type = "Intensywny Bootcamp", SkillIncrease = 10, FatigueIncrease = 25 },
                new() { Type = "Trening Mentalny", SkillIncrease = 2, FatigueIncrease = 5 }
            };
            await context.Trainings.AddRangeAsync(trainings);
        }

        await context.SaveChangesAsync();
    }
}