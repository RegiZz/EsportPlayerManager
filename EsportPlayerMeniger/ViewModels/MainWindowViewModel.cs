using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase currentView;

    public PlayerViewModel PlayerViewModel { get; }
    public TournamentViewModel TournamentViewModel { get; }
    public TrainingViewModel TrainingViewModel { get; }

    public MainWindowViewModel(
        IPlayerService playerService,
        ITournamentService tournamentService,
        ITrainingService trainingService)
    {
        PlayerViewModel = new PlayerViewModel(playerService);
        TournamentViewModel = new TournamentViewModel(tournamentService, playerService);
        TrainingViewModel = new TrainingViewModel(trainingService, playerService);
        
        currentView = PlayerViewModel;
    }

    [RelayCommand]
    private void ShowPlayers()
    {
        CurrentView = PlayerViewModel;
    }

    [RelayCommand]
    private void ShowTournaments()
    {
        CurrentView = TournamentViewModel;
    }

    [RelayCommand]
    private void ShowTraining()
    {
        CurrentView = TrainingViewModel;
    }
}