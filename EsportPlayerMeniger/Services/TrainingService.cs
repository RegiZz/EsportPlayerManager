using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsportPlayerMeniger.Data;
using EsportPlayerMeniger.Models;
using Microsoft.EntityFrameworkCore;

namespace EsportPlayerMeniger.Services;

public class TrainingService : ITrainingService
{
    private readonly AppDbContext _context;
    private readonly IPlayerService _playerService;
    private readonly Random _random = new();

    public TrainingService(AppDbContext context, IPlayerService playerService)
    {
        _context = context;
        _playerService = playerService;
    }

    // Pobiera wszystkie dostępne treningi
    public async Task<List<Training>> GetAllTrainingsAsync() =>
        await _context.Trainings.ToListAsync();

    // Pobiera trening po ID
    public async Task<Training?> GetTrainingByIdAsync(int id) =>
        await _context.Trainings.FindAsync(id);

    // Dodaje nowy trening do bazy danych
    public async Task AddTrainingAsync(Training training)
    {
        await _context.Trainings.AddAsync(training);
        await _context.SaveChangesAsync();
    }

    // Usuwa trening (opcjonalnie)
    public async Task DeleteTrainingAsync(Training training)
    {
        _context.Trainings.Remove(training);
        await _context.SaveChangesAsync();
    }

    // Sprawdza, czy gracz może trenować
    public Task<bool> CanPlayerTrain(Player player) =>
        Task.FromResult(player.FatigueLevel < 80 && player.StressLevel < 75);

    // Wykonuje trening dla gracza
    public async Task<TrainingResult> TrainPlayer(Player player, Training training)
    {
        var result = new TrainingResult();

        // Sprawdzamy, czy gracz spełnia warunki
        if (!await CanPlayerTrain(player))
        {
            result.Success = false;
            result.Message = "Gracz jest zbyt zmęczony lub zestresowany, aby trenować.";
            return result;
        }

        // Modyfikacje statystyk
        player.SkillLevel = Math.Min(100, player.SkillLevel + training.SkillIncrease);
        player.FatigueLevel = Math.Min(100, player.FatigueLevel + training.FatigueIncrease);
        player.StressLevel = Math.Min(100, player.StressLevel + _random.Next(2, 6)); // Losowy wzrost stresu

        // Możesz dodać koszt pieniężny treningu:
        player.Money -= training.Cost;

        await _playerService.UpdatePlayerAsync(player);

        result.Success = true;
        result.Message = $"Gracz {player.Nickname} ukończył trening '{training.Type}'.";
        result.SkillGained = training.SkillIncrease;
        result.FatigueIncreasedBy = training.FatigueIncrease;

        return result;
    }
}