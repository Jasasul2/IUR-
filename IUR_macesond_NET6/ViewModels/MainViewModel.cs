using IUR_macesond_NET6.Support;
using IUR_macesond_NET6.Models;
using IUR_macesond_NET6.ValidationRules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace IUR_macesond_NET6.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        // This Main View Model consists of both the user settings and the Task lists for every saved day

        public UserSettingsViewModel UserSettings { get; set; } = new UserSettingsViewModel();

        #region LevelProperties
        private int _currentLevel;
        private int _currentXP;
        private int _nextLevelXP;

        public int CurrentLevel
        {
            get => _currentLevel;
            set => SetProperty(ref _currentLevel, value);
        }

        public int CurrentXP
        {
            get => _currentXP;
            set => SetProperty(ref _currentXP, value);
        }

        public int NextLevelXP
        {
            get => _nextLevelXP;
            set => SetProperty(ref _nextLevelXP, value);
        }
        #endregion

        //    //public ObservableCollection<WeatherCardViewModel> WeatherCards { get; set; } = new ObservableCollection<WeatherCardViewModel>();
        //    private RelayCommand _AddTaskCommand;
        //    public RelayCommand AddTaskCommand
        //    {
        //        get { return _AddTaskCommand ?? (_AddTaskCommand = new RelayCommand(AddTask, AddTaskCanExecute)); }
        //    }

        //    private void AddTask(object obj)
        //    {
        //        //WeatherCards.Add(new WeatherCardViewModel(this, CityToBeAdded));
        //        //CityToBeAdded = "";
        //    }

        //    private bool AddTaskCanExecute(object obj)
        //    {
        //        return true;
        //    }

        public MainViewModel()
        {
            CurrentLevel = 5;
            CurrentXP = 32;
            NextLevelXP = 100;
        }
    }
}
