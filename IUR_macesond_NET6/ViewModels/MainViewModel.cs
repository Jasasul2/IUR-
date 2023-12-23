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
                IsNotFirstDate = (SelectedDate.Date > FirstDate.Date);

                // Updating the task list
                DateOnly dateOnly = DateOnly.FromDateTime(value);
                if (!DateToTaskListDictionary.ContainsKey(dateOnly))
                {
                    DateToTaskListDictionary.Add(dateOnly, new ObservableCollection<TaskViewModel>());
                } 
                SelectedTaskList = DateToTaskListDictionary[dateOnly];

                // TEST LINES === Generating random tasks
                if (SelectedTaskList.Count != 0) return;

                for (int i = 0; i < value.Day; i++)
                {
                    TaskViewModel newTask = new TaskViewModel(this);
                    newTask.TaskName = "Task " + value.Month + " " + value.Day +" "+ (i + 1);

                    Array values = Enum.GetValues(typeof(Difficulty));
                    Random random = new Random();
                    newTask.TaskDifficulty = (Difficulty)values.GetValue(random.Next(values.Length));

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
            PreviousDayCommand.RaiseCanExecuteChanged();
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
            PreviousDayCommand.RaiseCanExecuteChanged();
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

        private bool _isTaskSelected;

        public bool IsTaskSelected
        {
            get => _isTaskSelected;
            set => SetProperty(ref _isTaskSelected, value);
        }

        private TaskViewModel _selectedTask;

        public TaskViewModel SelectedTask
        {
            get => _selectedTask;
            set 
            { 
                SetProperty(ref _selectedTask, value);
                IsTaskSelected = (value != null);
            }
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
            SelectedTask.ResetAttributes();
        }

        private bool ResetSelectedTaskCommandCanExecute (object obj)
        {
            return (SelectedTask != null && !SelectedTask.Deprecated && !SelectedTask.MarkedForCompletion);
        }

        #endregion

        #region DeleteTaskCommand

        private RelayCommand _deleteTaskCommand;

        public RelayCommand DeleteTaskCommand
        {
            get { return _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand(DeleteSelectedTask, DeleteTaskCommandCanExecute)); }
        }

        private void DeleteSelectedTask(object obj)
        {
            SelectedTaskList.Remove(SelectedTask);
            SelectedTask = null;
        }

        private bool DeleteTaskCommandCanExecute(object obj)
        {
            return (SelectedTask != null && !SelectedTask.Deprecated && !SelectedTask.MarkedForCompletion);
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
            ResetSelectedTaskCommand.RaiseCanExecuteChanged();
        }

        private bool CompleteTaskCommandCanExecute(object obj)
        {
            return (SelectedTask != null && !SelectedTask.Deprecated);
        }
        #endregion

        #region DeleteGivenTask

        public void DeleteTask(TaskViewModel taskToDelete)
        {
            if(SelectedTask == taskToDelete)
            {
                SelectedTask = null;
            }
            SelectedTaskList.Remove(taskToDelete);
        }

        #endregion

        #region NameSorting

        private bool ascendingName = true;
        private RelayCommand _sortTasksByNameCommand;

        public RelayCommand SortTasksByNameCommand
        {
            get { return _sortTasksByNameCommand ?? (_sortTasksByNameCommand = new RelayCommand(SortTasksByName, SortTasksByNameCommandCanExecute)); }
        }

        private bool SortTasksByNameCommandCanExecute(object obj)
        {
            return (SelectedTaskList.Count > 0);
        }
        public void SortTasksByName(object obj)
        {
            if (ascendingName)
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.TaskName));
            }
            else
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.TaskName));
            }
            ascendingName = !ascendingName;
        }

        #endregion
        #region DifficultySorting

        private bool ascendingDiff = true;  
        private RelayCommand _sortTasksByDifficultyCommand;

        public RelayCommand SortTasksByDifficultyCommand
        {
            get { return _sortTasksByDifficultyCommand ?? (_sortTasksByDifficultyCommand = new RelayCommand(SortTasksByDifficulty, SortTasksByDifficultyCommandCanExecute)); }
        }

        private bool SortTasksByDifficultyCommandCanExecute(object obj)
        {
            return (SelectedTaskList.Count > 0);
        }

        public void SortTasksByDifficulty(object obj)
        {
            if (ascendingDiff)
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.TaskDifficulty));
            }
            else
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.TaskDifficulty));
            }
            ascendingDiff = !ascendingDiff;
        }

        #endregion
        #region CompletionSorting

        private bool ascendingComp = false;
        private RelayCommand _sortTasksByCompletionCommand;

        public RelayCommand SortTasksByCompletionCommand
        {
            get { return _sortTasksByCompletionCommand ?? (_sortTasksByCompletionCommand = new RelayCommand(SortTasksByCompletion, SortTasksByCompletionCommandCanExecute)); }
        }

        private bool SortTasksByCompletionCommandCanExecute(object obj)
        {
            return (SelectedTaskList.Count > 0);
        }

        public void SortTasksByCompletion(object obj)
        {
            if (ascendingComp)
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.MarkedForCompletion));
            }
            else
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.MarkedForCompletion));
            }
            ascendingComp = !ascendingComp;
        }

        #endregion
        #region TimeSorting

        private bool ascendingTime = true;
        private RelayCommand _sortTasksByTimeCommand;

        public RelayCommand SortTasksByTimeCommand
        {
            get { return _sortTasksByTimeCommand ?? (_sortTasksByTimeCommand = new RelayCommand(SortTasksByTime, SortTasksByTimeCommandCanExecute)); }
        }

        private bool SortTasksByTimeCommandCanExecute(object obj)
        {
            return (SelectedTaskList.Count > 0);
        }

        public void SortTasksByTime(object obj)
        {
            if (ascendingTime)
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.NotificationTime));
            }
            else
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.NotificationTime));
            }
            ascendingTime = !ascendingTime;
        }

        #endregion  
        #region RandomSorting   

        private RelayCommand _sortTasksRandomlyCommand;

        public RelayCommand SortTasksRandomlyCommand
        {
            get { return _sortTasksRandomlyCommand ?? (_sortTasksRandomlyCommand = new RelayCommand(SortTasksRandomly, SortTasksRandomlyCommandCanExecute)); }
        }

        private bool SortTasksRandomlyCommandCanExecute(object obj)
        {
            return (SelectedTaskList.Count > 0);
        }

        public void SortTasksRandomly(object obj)
        {
            SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => Guid.NewGuid()));
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
            SelectedTask = SelectedTaskList[0];

            //SelectedTask = null
            //IsTaskSelected = false;
        }
    }
}
