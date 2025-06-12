using EsportPlayerMeniger.Models;
using Microsoft.EntityFrameworkCore;

namespace EsportPlayerMeniger.Data;

public class AppDbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Training> Trainings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("Host=localhost;Database=esport;Username=postgres;Password=password123");
    }
}