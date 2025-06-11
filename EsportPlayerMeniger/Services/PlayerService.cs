using EsportPlayerMeniger.Data;
using EsportPlayerMeniger.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsportPlayerMeniger.Data;
using EsportPlayerMeniger.Models;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.Services;

public class PlayerService : IPlayerService
{
    private readonly AppDbContext _context;

    public PlayerService(AppDbContext context) => _context = context;

    public async Task<List<Player>> GetAllPlayersAsync() =>
        await _context.Players.ToListAsync();

    public async Task<Player?> GetPlayerByIdAsync(int id) =>
        await _context.Players.FindAsync(id);

    public async Task AddPlayerAsync(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePlayerAsync(Player player)
    {
        _context.Players.Update(player);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePlayerAsync(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player != null)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Player>> GetLeaderboardAsync() =>
        await _context.Players
            .OrderByDescending(p => p.RankingPoints)
            .Take(10)
            .ToListAsync();
}