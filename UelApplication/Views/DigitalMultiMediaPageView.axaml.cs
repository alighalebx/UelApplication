using Avalonia.Controls;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using UelApplication.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.VisualTree;
using UelApplication.Models;
using UelApplication.ViewModels;
using Section = UelApplication.Models.Section;
namespace UelApplication.Views;

public partial class DigitalMultiMediaPageView : UserControl
{
    public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();
    public DigitalMultiMediaPageView()
    {
        InitializeComponent();
        DataContext = new DigitalMultiMediaPageViewModel(); // Ensure this is set

    }
    private void AddSectionButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as DigitalMultiMediaPageViewModel;
        viewModel?.AddSection();
    }

    private async void UploadFilesButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            AllowMultiple = true
        };

        var parentWindow = this.GetVisualRoot() as Window;

        if (parentWindow != null)
        {
            var result = await dialog.ShowAsync(parentWindow);
            if (result != null && result.Length > 0)
            {
                var button = sender as Button;
                var section = button.DataContext as Section;

                var viewModel = DataContext as DigitalMultiMediaPageViewModel;
                viewModel?.UploadFiles(section, result);
            }
        }
    }

    private void DeleteSectionButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var section = button.DataContext as Section;

        var viewModel = DataContext as DigitalMultiMediaPageViewModel;
        viewModel?.DeleteSection(section);
    }
    private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var file = button.DataContext as string;
        var section = button.GetVisualParent<ListBox>().DataContext as Section;

        var viewModel = DataContext as DigitalMultiMediaPageViewModel;
        viewModel?.RemoveFile(section, file);
    }
    
    // TODO: replace with MVVM pattern (https://github.com/AvaloniaUI/Avalonia/issues/3766)
    
}
