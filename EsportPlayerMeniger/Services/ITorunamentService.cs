using System.Collections.Generic;
using System.Threading.Tasks;
using EsportPlayerMeniger.Models;

namespace EsportPlayerMeniger.Services;

public interface ITournamentService
{
    Task<List<Tournament>> GetAllTournamentsAsync();
    Task<Tournament?> GetTournamentByIdAsync(int id);
    Task AddTournamentAsync(Tournament tournament);
    Task<bool> CanPlayerJoinTournament(Player player, Tournament tournament);
    Task<TournamentResult> PlayTournament(Player player, Tournament tournament);
}

public class TournamentResult
{
    public bool Won { get; set; }
    public decimal PrizeWon { get; set; }
    public int PointsGained { get; set; }
    public string Message { get; set; } = string.Empty;
}