using KK.Models.Entities;
using KK.Models.Helpers;
using KK.Models.Repositories;
using System.Collections.ObjectModel;
using System.Windows;

namespace KK.ViewModels
{
    internal class MainViewModel : ObservableObject
    {

        private object _currentView;

        public StartViewModel StartVM { get; set; }
        public OverviewViewModel OverviewVM { get; set; }
        public CustomerViewModel CustomerVM { get; set; }

        public RelayCommand StartViewCommand { get; set; }
        public RelayCommand OverviewViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }

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
            CustomerVM = new CustomerViewModel();

            CurrentView = StartVM;

            StartViewCommand = new RelayCommand(o =>
            {
                CurrentView = StartVM;
            });
            OverviewViewCommand = new RelayCommand(o =>
            {
                CurrentView = OverviewVM;
            });
            CustomerViewCommand = new RelayCommand(o =>
            {
                CurrentView = CustomerVM;
            });
        }

    }
}

