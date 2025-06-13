using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerMeniger.Models;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.ViewModels;

public partial class TrainingViewModel : ViewModelBase
{
    private readonly ITrainingService _trainingService;
    private readonly IPlayerService _playerService;

    [ObservableProperty]
    private ObservableCollection<Training> trainings = new();

    [ObservableProperty]
    private ObservableCollection<Player> players = new();

    [ObservableProperty]
    private Training? selectedTraining;

    [ObservableProperty]
    private Player? selectedPlayer;

    [ObservableProperty]
    private bool canTrain;

    [ObservableProperty]
    private string trainingMessage = string.Empty;

    [ObservableProperty]
    private int entryFee;
    
    [ObservableProperty] 
    private int minSkillLevel;
    
    [ObservableProperty] 
    private int fatigueLevel;

    public TrainingViewModel(ITrainingService trainingService, IPlayerService playerService)
    {
        _trainingService = trainingService;
        _playerService = playerService;
        LoadDataAsync();
    }

    partial void OnSelectedPlayerChanged(Player? value)
    {
        CheckCanTrain();
    }

    private async void CheckCanTrain()
    {
        if (SelectedPlayer != null)
        {
            CanTrain = await _trainingService.CanPlayerTrain(SelectedPlayer);
            TrainingMessage = CanTrain ? "Gracz może trenować" : "Gracz jest zbyt zmęczony do treningu";
        }
        else
        {
            CanTrain = false;
            TrainingMessage = string.Empty;
        }
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        var trainingList = await _trainingService.GetAllTrainingsAsync();
        var playerList = await _playerService.GetAllPlayersAsync();

        Trainings.Clear();
        Players.Clear();

        foreach (var training in trainingList)
        {
            Trainings.Add(training);
        }

        foreach (var player in playerList)
        {
            Players.Add(player);
        }
    }

    [RelayCommand]
    private async Task TrainPlayerAsync()
    {
        if (SelectedPlayer == null || SelectedTraining == null)
            return;

        await _trainingService.TrainPlayer(SelectedPlayer, SelectedTraining);
        TrainingMessage = $"Trening {SelectedTraining.Type} zakończony! Skill zwiększony o {SelectedTraining.SkillIncrease}";

        // Odświeżamy dane
        await LoadDataAsync();
    }
}