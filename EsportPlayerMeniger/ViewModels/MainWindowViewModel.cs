using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerMeniger.Services;
using EsportPlayerMeniger.Data;

namespace EsportPlayerMeniger.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentView;

    public PlayerViewModel PlayerViewModel { get; }
    public TournamentViewModel TournamentViewModel { get; }
    public TrainingViewModel TrainingViewModel { get; }

    public MainWindowViewModel()
    {
        // Create DbContext and Services
        var dbContext = new AppDbContext();
        var playerService = new PlayerService(dbContext);
        var tournamentService = new TournamentService(dbContext, playerService);
        var trainingService = new TrainingService(dbContext, playerService);

        // Initialize ViewModels
        PlayerViewModel = new PlayerViewModel(playerService);
        TournamentViewModel = new TournamentViewModel(tournamentService, playerService);
        TrainingViewModel = new TrainingViewModel(trainingService, playerService);
        
        currentView = PlayerViewModel;
        

        // Initialize database
        InitializeDatabaseAsync();
    }

    private async void InitializeDatabaseAsync()
    {
        using var context = new AppDbContext();
        await DatabaseSeeder.SeedAsync(context);
        
    }

    [RelayCommand]
    private void ShowPlayers() => CurrentView = PlayerViewModel;

    [RelayCommand]
    private void ShowTournaments() => CurrentView = TournamentViewModel;

    [RelayCommand]
    private void ShowTraining() => CurrentView = TrainingViewModel;
}