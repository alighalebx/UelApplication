﻿using Avalonia.Controls;
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

public partial class CyberSecurityPageView : UserControl
{
    public ObservableCollection<Section> Sections { get; } = new ObservableCollection<Section>();

    public CyberSecurityPageView()
    {
        InitializeComponent();
        DataContext = new CyberSecurityPageViewModel(); // Ensure this is set

    }

    private void AddSectionButton_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = DataContext as CyberSecurityPageViewModel;
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

                var viewModel = DataContext as CyberSecurityPageViewModel;
                viewModel?.UploadFiles(section, result);
            }
        }
    }

    private void DeleteSectionButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var section = button.DataContext as Section;

        var viewModel = DataContext as CyberSecurityPageViewModel;
        viewModel?.DeleteSection(section);
    }

    private void RemoveFileButton_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        var file = button.DataContext as string;
        var section = button.GetVisualParent<ListBox>().DataContext as Section;

        var viewModel = DataContext as CyberSecurityPageViewModel;
        viewModel?.RemoveFile(section, file);
    }
}
