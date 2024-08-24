using CommunityToolkit.Mvvm.ComponentModel;

namespace UelApplication.ViewModels;

public partial class CyberSecurityPageViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isValueSelectionEnabled = true;

    [ObservableProperty]
    private int _sliderValue;
}