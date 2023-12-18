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
using System.Printing;

namespace IUR_macesond_NET6.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        // This Main View Model consists of both the user settings and the Task lists for every saved day

        public UserSettingsViewModel UserSettings { get; set; }
        public LocalizedText LocalizedText { get; set; }

        #region DateBindingAndCommands

        // The first date user launched this application. He cannot go past it.
        private DateOnly _firstDate;

        public DateTime FirstDate
        {
            get => _firstDate.ToDateTime(new TimeOnly());
            set => SetProperty(ref _firstDate, DateOnly.FromDateTime(value));
        }

        private DateOnly _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate.ToDateTime(new TimeOnly());
            set
            {
                SetProperty(ref _selectedDate, DateOnly.FromDateTime(value));

                // Updating the task list
                DateOnly dateOnly = DateOnly.FromDateTime(value);
                if (!DateToTaskListDictionary.ContainsKey(dateOnly))
                {
                    DateToTaskListDictionary.Add(dateOnly, new ObservableCollection<TaskViewModel>());
                } 
                SelectedTaskList = DateToTaskListDictionary[dateOnly];

                // TEST LINES
                if (SelectedTaskList.Count != 0) return;

                for (int i = 0; i < value.Day; i++)
                {
                    TaskViewModel newTask = new TaskViewModel(this);
                    newTask.TaskName = "Task " + i;
                    SelectedTaskList.Add(newTask);
                }
            }
        }

        private bool _isNotFirstDate;
        public bool IsNotFirstDate
        {
            get => (_isNotFirstDate);
            set => SetProperty(ref _isNotFirstDate, value);
        }

        private RelayCommand _previousDayCommand;

        public RelayCommand PreviousDayCommand
        {
            get { return _previousDayCommand ?? (_previousDayCommand = new RelayCommand(PreviousDay, PreviousDayCommandCanExecute)); }
        }

        private void PreviousDay(object obj)
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        private bool PreviousDayCommandCanExecute(object obj)
        {
            IsNotFirstDate = (SelectedDate.Date > FirstDate.Date);
            return IsNotFirstDate;
        }

        private RelayCommand _nextDayCommand;

        public RelayCommand NextDayCommand
        {
            get { return _nextDayCommand ?? (_nextDayCommand = new RelayCommand(NextDay, NextDayCommandCanExecute)); }
        }

        private bool NextDayCommandCanExecute(object obj)
        {
            return true;
        }

        private void NextDay(object obj)
        {
            SelectedDate = SelectedDate.AddDays(1); 
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

        // The currently selected visible task list in the left window 

        private ObservableCollection<TaskViewModel> _selectedTaskList;
        public ObservableCollection<TaskViewModel> SelectedTaskList
        {
            get => _selectedTaskList;
            set => SetProperty(ref _selectedTaskList, value);
        }

        private Dictionary<DateOnly, ObservableCollection<TaskViewModel>> DateToTaskListDictionary = new Dictionary<DateOnly, ObservableCollection<TaskViewModel>>();


        #endregion

        #region ResetCommand

        private RelayCommand _resetSelectedTaskCommand;

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

        #endregion

        #region CompleteTaskTestCommand

        private RelayCommand _completeTaskCommand;

        public RelayCommand CompleteTaskCommand
        {
            get { return _completeTaskCommand ?? (_completeTaskCommand = new RelayCommand(CompleteSelectedTask, CompleteTaskCommandCanExecute)); }
        }

        private void CompleteSelectedTask(object obj)
        {
            if (SelectedTask != null)
            {
                SelectedTask.Complete();
            }
        }

        private bool CompleteTaskCommandCanExecute(object obj)
        {
            return (SelectedTask != null);
        }
        #endregion

        public MainViewModel()
        {
            UserSettings = new UserSettingsViewModel(this);
            LocalizedText = new LocalizedText(UserSettings.CurrentLanguage);

            // Date Init
            FirstDate = DateTime.Now;

            SelectedDate = DateTime.Now;

            // XP Init
            CurrentLevel = 1;
            CurrentXP = 5;
            NextLevelXP = 10;

            // Task Init
            SelectedTask = new TaskViewModel(this);
        }
    }
}
