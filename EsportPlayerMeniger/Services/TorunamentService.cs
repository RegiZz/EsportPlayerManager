using EsportPlayerMeniger.Models;
using EsportPlayerMeniger.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsportPlayerMeniger.Data;
using EsportPlayerMeniger.Models;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.Services;

public class TournamentService : ITournamentService
{
    private readonly AppDbContext _context;
    private readonly IPlayerService _playerService;
    private readonly Random _random = new();

    public TournamentService(AppDbContext context, IPlayerService playerService)
    {
        _context = context;
        _playerService = playerService;
    }

    public async Task<List<Tournament>> GetAllTournamentsAsync() =>
        await _context.Tournaments.ToListAsync();

    public async Task<Tournament?> GetTournamentByIdAsync(int id) =>
        await _context.Tournaments.FindAsync(id);

    public async Task AddTournamentAsync(Tournament tournament)
    {
        _context.Tournaments.Add(tournament);
        await _context.SaveChangesAsync();
    }

    public Task<bool> CanPlayerJoinTournament(Player player, Tournament tournament) =>
        Task.FromResult(
            player.SkillLevel >= tournament.MinSkillRequired &&
            player.Money >= tournament.EntryFee &&
            player.FatigueLevel < 90
        );

    public async Task<TournamentResult> PlayTournament(Player player, Tournament tournament)
    {
        player.Money -= tournament.EntryFee;

        var baseChance = Math.Min(0.8, player.SkillLevel / 100.0);
        var fatigueReduction = player.FatigueLevel / 100.0 * 0.3;
        var stressReduction = player.StressLevel / 100.0 * 0.2;
        var winChance = Math.Max(0.1, baseChance - fatigueReduction - stressReduction);

        var won = _random.NextDouble() < winChance;
        var result = new TournamentResult();

        if (won)
        {
            result.Won = true;
            result.PrizeWon = tournament.PrizePool;
            result.PointsGained = tournament.MinSkillRequired * 10;
            result.Message = $"Gratulacje! Wygrałeś turniej {tournament.Name}!";
            player.Money += tournament.PrizePool;
            player.RankingPoints += result.PointsGained;
        }
        else
        {
            result.Won = false;
            result.PrizeWon = 0;
            result.PointsGained = 0;
            result.Message = $"Przegrałeś turniej {tournament.Name}. Spróbuj ponownie!";
        }

        player.FatigueLevel = Math.Min(100, player.FatigueLevel + 15);
        player.StressLevel = Math.Min(100, player.StressLevel + 10);

        await _playerService.UpdatePlayerAsync(player);

        return result;
    }
}