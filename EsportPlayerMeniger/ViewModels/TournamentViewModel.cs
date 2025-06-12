using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerMeniger.Models;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.ViewModels;

public partial class TournamentViewModel : ViewModelBase
{
    private readonly ITournamentService _tournamentService;
    private readonly IPlayerService _playerService;

    [ObservableProperty]
    private ObservableCollection<Tournament> tournaments = new();

    [ObservableProperty]
    private ObservableCollection<Player> players = new();

    [ObservableProperty]
    private Tournament? selectedTournament;

    [ObservableProperty]
    private Player? selectedPlayer;

    [ObservableProperty]
    private string tournamentResult = string.Empty;

    [ObservableProperty]
    private bool canJoinTournament;
    
    [ObservableProperty]
    private string newTournamentName = string.Empty;

    [ObservableProperty]
    private int newTournamentMinSkill;

    [ObservableProperty]
    private decimal newTournamentEntryFee;

    [ObservableProperty]
    private decimal newTournamentPrizePool;
    
    

    public TournamentViewModel(ITournamentService tournamentService, IPlayerService playerService)
    {
        _tournamentService = tournamentService;
        _playerService = playerService;
        LoadDataAsync();
    }

    partial void OnSelectedTournamentChanged(Tournament? value) => CheckCanJoinTournament();
    partial void OnSelectedPlayerChanged(Player? value) => CheckCanJoinTournament();

    private async void CheckCanJoinTournament()
    {
        if (SelectedPlayer != null && SelectedTournament != null)
        {
            CanJoinTournament = await _tournamentService.CanPlayerJoinTournament(SelectedPlayer, SelectedTournament);
        }
        else
        {
            CanJoinTournament = false;
        }
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        var tournamentList = await _tournamentService.GetAllTournamentsAsync();
        var playerList = await _playerService.GetAllPlayersAsync();

        Tournaments.Clear();
        Players.Clear();

        foreach (var tournament in tournamentList)
        {
            Tournaments.Add(tournament);
        }

        foreach (var player in playerList)
        {
            Players.Add(player);
        }
    }

    [RelayCommand]
    private async Task JoinTournamentAsync()
    {
        if (SelectedPlayer == null || SelectedTournament == null)
            return;

        var result = await _tournamentService.PlayTournament(SelectedPlayer, SelectedTournament);
        TournamentResult = result.Message;

        await LoadDataAsync(); // Odśwież dane gracza po turnieju
    }

    // Dodawanie turnieju
    [RelayCommand]
    private async Task AddNewTournamentAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTournamentName) ||
            NewTournamentMinSkill < 0 ||
            NewTournamentEntryFee < 0 ||
            NewTournamentPrizePool < 0)
        {
            TournamentResult = "Wypełnij wszystkie pola poprawnie.";
            return;
        }

        var tournament = new Tournament
        {
            Name = NewTournamentName,
            MinSkillRequired = NewTournamentMinSkill,
            EntryFee = NewTournamentEntryFee,
            PrizePool = NewTournamentPrizePool
        };

        await _tournamentService.AddTournamentAsync(tournament);

        TournamentResult = $"✅ Turniej '{tournament.Name}' został dodany!";
        await LoadDataAsync(); // Odśwież listę turniejów

        // Wyczyść pola formularza
        NewTournamentName = string.Empty;
        NewTournamentMinSkill = 0;
        NewTournamentEntryFee = 0;
        NewTournamentPrizePool = 0;
    }
}