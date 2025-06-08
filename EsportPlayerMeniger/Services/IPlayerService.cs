using System.Collections.Generic;
using System.Threading.Tasks;
using EsportPlayerMeniger.Models;

namespace EsportPlayerMeniger.Services;

public interface IPlayerService
{
    Task<List<Player>> GetAllPlayersAsync();
    Task<Player?> GetPlayerByIdAsync(int id);
    Task AddPlayerAsync(Player player);
    Task UpdatePlayerAsync(Player player);
    Task DeletePlayerAsync(int id);
    Task<List<Player>> GetLeaderboardAsync();
}