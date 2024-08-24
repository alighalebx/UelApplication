using CommunityToolkit.Mvvm.ComponentModel;

namespace UelApplication.ViewModels;

public partial class ArtificialIntelligencePageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isButtonEnabled = true;
}