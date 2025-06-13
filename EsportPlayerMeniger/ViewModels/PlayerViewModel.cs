using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerMeniger.Models;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
    private readonly IPlayerService _playerService;

    [ObservableProperty]
    private ObservableCollection<Player> _players = new();

    [ObservableProperty]
    private ObservableCollection<Player> _leaderboard = new();

    [ObservableProperty]
    private Player? _selectedPlayer;

    [ObservableProperty]
    private string _newPlayerNickname = string.Empty;

    [ObservableProperty]
    private string _newPlayerGame = string.Empty;

    [ObservableProperty]
    private bool _isAddingPlayer;

    public PlayerViewModel(IPlayerService playerService)
    {
        _ = LoadPlayersAsync();
        _playerService = playerService;
    }

    [RelayCommand]
    public async Task LoadPlayersAsync()
    {
        var playerList = await _playerService.GetAllPlayersAsync();
        Players.Clear();
        foreach (var player in playerList)
        {
            Players.Add(player);
        }
    }

    [RelayCommand]
    private async Task LoadLeaderboardAsync()
    {
        var leaderboardList = await _playerService.GetLeaderboardAsync();
        Leaderboard.Clear();
        foreach (var player in leaderboardList)
        {
            Leaderboard.Add(player);
        }
    }

    [RelayCommand]
    private void ShowAddPlayerDialog()
    {
        NewPlayerNickname = string.Empty;
        NewPlayerGame = string.Empty;
        IsAddingPlayer = true;
    }

    [RelayCommand]
    private void CancelAddPlayer()
    {
        IsAddingPlayer = false;
        NewPlayerNickname = string.Empty;
        NewPlayerGame = string.Empty;
    }

    [RelayCommand]
    private async Task AddPlayerAsync()
    {
        if (string.IsNullOrWhiteSpace(NewPlayerNickname) || string.IsNullOrWhiteSpace(NewPlayerGame))
            return;

        var newPlayer = new Player
        {
            Nickname = NewPlayerNickname,
            Game = NewPlayerGame,
            SkillLevel = 30,
            StressLevel = 0,
            FatigueLevel = 0,
            RankingPoints = 0,
            Money = 1069
        };

        await _playerService.AddPlayerAsync(newPlayer);
        await LoadPlayersAsync();
        await LoadLeaderboardAsync();
        
        IsAddingPlayer = false;
        NewPlayerNickname = string.Empty;
        NewPlayerGame = string.Empty;
    }

    [RelayCommand]
    private async Task DeletePlayerAsync(Player? player)
    {
        if (player == null) return;

        await _playerService.DeletePlayerAsync(player.Id);
        await LoadPlayersAsync();
        await LoadLeaderboardAsync();
    }

    [RelayCommand]
    private async Task RestPlayerAsync(Player? player)
    {
        if (player == null) return;

        player.FatigueLevel = Math.Max(0, player.FatigueLevel - 20);
        player.StressLevel = Math.Max(0, player.StressLevel - 15);
        
        await _playerService.UpdatePlayerAsync(player);
        await LoadPlayersAsync();
    }
}