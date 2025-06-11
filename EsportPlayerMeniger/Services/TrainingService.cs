using System;
using System.Collections.Generic;
using EsportPlayerMeniger.Data;
using EsportPlayerMeniger.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EsportPlayerMeniger.Services;

public class TrainingService : ITrainingService
{
    private readonly AppDbContext _context;
    private readonly IPlayerService _playerService;

    public TrainingService(AppDbContext context, IPlayerService playerService)
    {
        _context = context;
        _playerService = playerService;
    }

    public async Task<List<Training>> GetAllTrainingsAsync() =>
        await _context.Trainings.ToListAsync();

    public async Task<Training?> GetTrainingByIdAsync(int id) =>
        await _context.Trainings.FindAsync(id);

    public async Task AddTrainingAsync(Training training)
    {
        _context.Trainings.Add(training);
        await _context.SaveChangesAsync();
    }

    public Task<bool> CanPlayerTrain(Player player) =>
        Task.FromResult(player.FatigueLevel < 80);

    public async Task TrainPlayer(Player player, Training training)
    {
        player.SkillLevel = Math.Min(100, player.SkillLevel + training.SkillIncrease);
        player.FatigueLevel = Math.Min(100, player.FatigueLevel + training.FatigueIncrease);
        await _playerService.UpdatePlayerAsync(player);
    }
}