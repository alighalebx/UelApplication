using CommunityToolkit.Mvvm.ComponentModel;

namespace UelApplication.ViewModels;

public partial class DigitalMultiMediaPageViewModel: ViewModelBase
{
    [ObservableProperty]
    private bool _isTextEnabled = true;
}