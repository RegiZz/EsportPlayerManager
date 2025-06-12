using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using EsportPlayerMeniger.Data;
using EsportPlayerMeniger.Models;

namespace EsportPlayerMeniger.Views;

public partial class PlayerView : UserControl
{
    public PlayerView()
    {
        InitializeComponent();
    }

    private void AddNewPlayerButton(object? sender, RoutedEventArgs e)
    {
        var newPlayer = new Player
        {
            Nickname = "Gracz" + new Random().Next(1000),
            Game = "CS2",
            SkillLevel = 50,
            StressLevel = 20,
            FatigueLevel = 10,
            Money = 1000,
            RankingPoints = 1000
        };

        using var context = new AppDbContext();
        context.Players.AddAsync(newPlayer);
        context.SaveChangesAsync();

        Console.WriteLine($"Gracz {newPlayer.Nickname} został zapisany do bazy.");
    }
}