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
using System.Windows;

namespace IUR_macesond_NET6.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        // This Main View Model consists of both the user settings and the Task lists for every saved day

        public UserSettingsViewModel UserSettings { get; set; }
        public LocalizedText LocalizedText { get; set; }

        #region DateBinding

        private DateOnly _selectedDate;
        public DateOnly SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        public String SelectedDateString
        {
            get => SelectedDate.ToString();
            set
            {
                if (DateOnly.TryParse(value, out DateOnly date))
                {
                    SelectedDate = date;
                }
                else
                {
                    SelectedDate = DateOnly.FromDateTime(DateTime.Now);
                }
            }
        }

        #endregion

        #region XPAndLevelProperties
        private readonly int XP_LEVEL_INCREASER = 5;

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

        private void LevelUp()
        {
            CurrentLevel++;
            NextLevelXP += XP_LEVEL_INCREASER;
        }

        public void AddEXP(int newXP)
        {
            CurrentXP += newXP;
            if (CurrentXP >= NextLevelXP)
            {
                CurrentXP = CurrentXP - NextLevelXP;
                LevelUp();
            }
        }
        #endregion

        #region TaskLists 

        private TaskViewModel _selectedTask;

        public TaskViewModel SelectedTask
        {
            get => _selectedTask;
            set => SetProperty(ref _selectedTask, value);
        }

        #endregion

        #region ResetCommand

        private RelayCommand _resetSelectedTaskCommand;
        private string _cityToBeAdded = "";

        public RelayCommand ResetSelectedTaskCommand
        {
            get { return _resetSelectedTaskCommand ?? (_resetSelectedTaskCommand = new RelayCommand(ResetSelectedTask, ResetSelectedTaskCommandCanExecute)); }
        }

        private void ResetSelectedTask(object obj)
        {
            if (SelectedTask != null)
            {
                SelectedTask.ResetAttributes();
            }
        }

        private bool ResetSelectedTaskCommandCanExecute (object obj)
        {
            return (SelectedTask != null);
        }

        public string CityToBeAdded
        {
            get => _cityToBeAdded;
            set => SetProperty(ref _cityToBeAdded, value);
        }

        #endregion


        public MainViewModel()
        {
            UserSettings = new UserSettingsViewModel(this);
            LocalizedText = new LocalizedText(UserSettings.CurrentLanguage);

            // Date Init
            SelectedDate = DateOnly.FromDateTime(DateTime.Now);

            // XP Init
            CurrentLevel = 1;
            CurrentXP = 5;
            NextLevelXP = 10;

            // Task Init
            SelectedTask = new TaskViewModel(this);
        }
    }
}
