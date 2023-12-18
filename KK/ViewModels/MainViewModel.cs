using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace KK.ViewModels
{
    internal class MainViewModel : ObservableObject
    {

        private object _currentView;

        private StartViewModel StartVM;
        private OverviewViewModel OverviewVM;

        // Public Propperties
        public RelayCommand StartViewCommand { get; set; }
        public RelayCommand OverviewViewCommand { get; set; }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            StartVM = new StartViewModel();
            OverviewVM = new OverviewViewModel();

            CurrentView = StartVM;

            StartViewCommand = new RelayCommand(o =>
            {
                CurrentView = StartVM;
            });
            OverviewViewCommand = new RelayCommand(o =>
            {
                CurrentView = OverviewVM;
            });

        }

    }
}

