﻿using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jajo.Exporter.Commands;
using Jajo.Exporter.Services;
using Jajo.Exporter.Stores;
using Jajo.Exporter.ViewModels.Pages;
using Jajo.Exporter.ViewModels.Utils;

namespace Jajo.Exporter.ViewModels;

public sealed partial class MainViewModel : ObservableValidator, IMainViewModel
{
    private readonly NavigationStore _navigationStore;
    
    private ICommand _onWindowLoadedCommand;
    public ICommand SetExportViewModelCommand { get; }
    public ICommand SetSchedularViewModelCommand { get; }

    public Action<string> ShowMessage { get; set; }
    public event EventHandler CloseRequested = delegate { }; // Invokes when the main window should be closed

    public MainViewModel(NavigationStore navigationStore)
    {
        // To see how navigation works and is implemented step by step
        // https://www.youtube.com/watch?v=N26C_Cq-gAY&list=PLA8ZIAm2I03ggP55JbLOrXl6puKw4rEb2

        // Registering navigation store and setting startup page
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));
        _navigationStore.CurrentViewModel = new ExportViewModel();

        // Registering navigation commands, so after clicking a radiobutton
        // it will invoke one of this command
        SetExportViewModelCommand = new NavigateCommand<ExportViewModel>(
            new NavigationService<ExportViewModel>(navigationStore, () => new ExportViewModel()));
        SetSchedularViewModelCommand =
            new NavigateCommand<SchedularViewModel>(
                new NavigationService<SchedularViewModel>(navigationStore, () => new SchedularViewModel()));
    }

    public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
    
    [RelayCommand]
    private void Close()
    {
        CloseRequested.Invoke(this,EventArgs.Empty);
    }

    // Here you can add code that will be executed before the window is shown
    public ICommand OnWindowLoadedCommand => _onWindowLoadedCommand ??= new RelayCommand(() =>
    {
    });

    public void OnApplicationClosing()
    {
    }
}