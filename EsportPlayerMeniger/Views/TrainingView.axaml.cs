using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using EsportPlayerMeniger.Services;

namespace EsportPlayerMeniger.Views;

public partial class TrainingView : UserControl
{

    public IPlayerService PlayerService;
    public TrainingView()
    {
        InitializeComponent();
        
    }
}