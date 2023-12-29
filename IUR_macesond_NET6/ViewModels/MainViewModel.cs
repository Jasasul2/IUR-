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
using Newtonsoft.Json.Linq;
using System.Windows.Controls.Primitives;
using IUR_macesond_NET6.Converters;

namespace IUR_macesond_NET6.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        // This Main View Model consists of both the user settings and the Task lists for every saved day

        private const int MAX_TASK_LIST_LENGTH = 10;
        private const int MAX_TASK_LIBRARY_LENGTH = 40;

        public ModelDataLoader ModelDataLoader { get; set; }
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

        private DateOnly _selectedDateTime;
        public DateTime SelectedDateTime
        {
            get => _selectedDateTime.ToDateTime(new TimeOnly());
            set
            {
                SetProperty(ref _selectedDateTime, DateOnly.FromDateTime(value));
                IsNotFirstDate = (SelectedDateTime.Date > FirstDate.Date);

                // Updating the task list
                DateOnly dateOnly = DateOnly.FromDateTime(value);
                if (!DateToTaskListDictionary.ContainsKey(dateOnly))
                {
                    DateToTaskListDictionary.Add(dateOnly, new ObservableCollection<TaskViewModel>());
                }
                SelectedTaskList = DateToTaskListDictionary[dateOnly];

                DayMatch = SelectedDateTime.Date == CurrentDateTime.Date;
                MarkTasksAsDeprecated();
                CountCompletedTasks();

                // Updating the current date to refresh the UI
                CurrentDateTime = DateTime.Now;
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
            SelectedDateTime = SelectedDateTime.AddDays(-1);
            PreviousDayCommand.RaiseCanExecuteChanged();
        }

        private bool PreviousDayCommandCanExecute(object obj)
        {
            IsNotFirstDate = (SelectedDateTime.Date > FirstDate.Date);
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
            SelectedDateTime = SelectedDateTime.AddDays(1);
            PreviousDayCommand.RaiseCanExecuteChanged();
        }

        private RelayCommand _todayCommand;

        public RelayCommand TodayCommand
        {
            get { return _todayCommand ?? (_todayCommand = new RelayCommand(Today, TodayCommandCanExecute)); }
        }

        private bool TodayCommandCanExecute(object obj)
        {
            return true;
        }

        private void Today(object obj)
        {
            SelectedDateTime = DateTime.Now;
            PreviousDayCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region XPAndLevelProperties
        private readonly int XP_LEVEL_INCREASER = 5;

        private int _totalPoints;

        public int TotalPoints
        {
            get => _totalPoints;
            set => SetProperty(ref _totalPoints, value);
        }

        public void AddPoints(int newPoints)
        {
            TotalPoints += newPoints;
        }
        #endregion

        #region TaskLists 

        public void SelectThisTask(TaskViewModel task)
        {
            if (SelectedTaskList == null) return;

            if(SelectedTaskList.Contains(task))
            {
                SelectedTask = task;
            } else
            {
                SelectedTemplate = task;
            }
        }

        // Selected Task 
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
                if (!IsTaskSelected) return;
                SelectedTemplate = null;
                IsTemplateSelected = false;
                CurrentDateTime = DateTime.Now;
            }
        }

        // Selected Task Template

        private bool _isTemplateSelected;

        public bool IsTemplateSelected
        {
            get => _isTemplateSelected;
            set => SetProperty(ref _isTemplateSelected, value);
        }

        private TaskViewModel _selectedTemplate;

        public TaskViewModel SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                SetProperty(ref _selectedTemplate, value);
                IsTemplateSelected = (value != null);
                if (!IsTemplateSelected) return;
                SelectedTask = null;
                IsTaskSelected = false;
            }
        }


        // The currently selected visible task list in the left window 

        private ObservableCollection<TaskViewModel> _selectedTaskList;
        public ObservableCollection<TaskViewModel> SelectedTaskList
        {
            get => _selectedTaskList;
            set {
                SetProperty(ref _selectedTaskList, value);
                UpdateTotalTasks();
            } 
        }

        private Dictionary<DateOnly, ObservableCollection<TaskViewModel>> DateToTaskListDictionary = new Dictionary<DateOnly, ObservableCollection<TaskViewModel>>();

        // Task Library

        private ObservableCollection<TaskViewModel> _taskLibrary;

        public ObservableCollection<TaskViewModel> TaskLibrary
        {
            get => _taskLibrary;
            set => SetProperty(ref _taskLibrary, value);
        }

        #endregion
        
        // This is the add button on the left (aka create new)
        #region AddTaskCommand

        private RelayCommand _addTaskCommand;

        public RelayCommand AddTaskCommand
        {
            get { return _addTaskCommand ?? (_addTaskCommand = new RelayCommand(AddTask, AddTaskCommandCanExecute)); }
        }

        private void AddTask(object obj)
        {
            TaskViewModel newTask = new TaskViewModel(this, new TaskModel());
            SelectedTaskList.Add(newTask);
            SelectedTask = newTask;
            UpdateTotalTasks();
        }

        private bool AddTaskCommandCanExecute(object obj)
        {
            return SelectedTaskList.Count < MAX_TASK_LIST_LENGTH && !CheckDeprecationCondition();
        }

        #endregion

        #region AddRandomTaskCommand

        private RelayCommand _addRandomTaskCommand;

        public RelayCommand AddRandomTaskCommand
        {
            get { return _addRandomTaskCommand ?? (_addRandomTaskCommand = new RelayCommand(AddRandomTask, AddRandomTaskCommandCanExecute)); }
        }

        private void AddRandomTask(object obj)
        {
            TaskModel newTask = new TaskModel();
            newTask.SetAttributes(TaskLibrary[new Random().Next(TaskLibrary.Count)]);
            TaskViewModel newTaskViewModel = new TaskViewModel(this, newTask);
            SelectedTaskList.Add(newTaskViewModel);
            SelectedTask = newTaskViewModel;
            UpdateTotalTasks();
        }

        private bool AddRandomTaskCommandCanExecute(object obj)
        {
            return SelectedTaskList.Count < MAX_TASK_LIST_LENGTH && TaskLibrary.Count > 0 && !CheckDeprecationCondition();  
        }


        #endregion

        #region AddTaskTemplateCommand


        private RelayCommand _addTaskTemplateCommand;

        public RelayCommand AddTaskTemplateCommand
        {
            get { return _addTaskTemplateCommand ?? (_addTaskTemplateCommand = new RelayCommand(AddTaskTemplate, AddTaskTemplateCommandCanExecute)); }
        }

        private void AddTaskTemplate(object obj)
        {
            TaskViewModel newTaskTemplate = new TaskViewModel(this, new TaskModel());
            TaskLibrary.Add(newTaskTemplate);
            SelectedTemplate = newTaskTemplate;
        }

        private bool AddTaskTemplateCommandCanExecute(object obj)
        {
            return TaskLibrary.Count < MAX_TASK_LIBRARY_LENGTH;
        }

        #endregion

        #region ResetCommand

        private RelayCommand _resetSelectedTaskCommand;

        public RelayCommand ResetSelectedTaskCommand
        {
            get { return _resetSelectedTaskCommand ?? (_resetSelectedTaskCommand = new RelayCommand(ResetSelectedTask, ResetSelectedTaskCommandCanExecute)); }
        }

        private void ResetSelectedTask(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if(type == "Task")
            {
                SelectedTaskList.Remove(SelectedTask);
                SelectedTask = null;
                AddTask(null);
                
//                SelectedTask.SetAttributes(new TaskModel());
            } else if (type == "Template")
            {
                TaskLibrary.Remove(SelectedTemplate);
                SelectedTemplate = null;
                AddTaskTemplate(null);
                //SelectedTemplate.SetAttributes(new TaskModel());
            }
        }

        private bool ResetSelectedTaskCommandCanExecute(object obj)
        {
            var type = obj as string;
            if (type == null) return false;

            if (type == "Task")
            {
                return (SelectedTask != null && !SelectedTask.Deprecated && !SelectedTask.MarkedForCompletion);
            }
            else if (type == "Template")
            {
                return (SelectedTemplate != null);
            }
            return false;
        }

        #endregion

        #region DeleteTaskCommand

        private RelayCommand _deleteTaskCommand;

        public RelayCommand DeleteTaskCommand
        {
            get { return _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand(DeleteSelectedTask, DeleteSelectedTaskCommandCanExecute)); }
        }

        private void DeleteSelectedTask(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if (type == "Task")
            {
                SelectedTaskList.Remove(SelectedTask);
                SelectedTask = null;
                UpdateTotalTasks();
            }
            else if (type == "Template")
            {
                TaskLibrary.Remove(SelectedTemplate);
                SelectedTemplate = null;
            }
        }

        private bool DeleteSelectedTaskCommandCanExecute(object obj)
        {
            var type = obj as string;
            if (type == null) return false;

            if (type == "Task")
            {
                return (SelectedTask != null && !SelectedTask.Deprecated && !SelectedTask.MarkedForCompletion);
            }
            else if (type == "Template")
            {
                return (SelectedTemplate != null);
            }
            return false;
        }

        #endregion

        #region DeleteGivenTask

        public void DeleteTask(TaskViewModel taskToDelete, string type)
        {
            if(type == "Task")
            {
                if (SelectedTask == taskToDelete)
                {
                    SelectedTask = null;
                }
                SelectedTaskList.Remove(taskToDelete);
                UpdateTotalTasks();
            } else if (type == "Template")
            {
                if (SelectedTemplate == taskToDelete)
                {
                    SelectedTemplate = null;
                }
                TaskLibrary.Remove(taskToDelete);
            }
        }

        #endregion

        #region UseTaskTemplateCommand

        private TaskViewModel CopyTask(TaskViewModel original)
        {
            TaskModel newTaskModel = new TaskModel();
            newTaskModel.SetAttributes(original);
            TaskViewModel newTask = new TaskViewModel(this, newTaskModel);
            return newTask;
        }

        public void UseTaskTemplate(TaskViewModel templateToAdd) 
        {
            SelectedTaskList.Add(CopyTask(templateToAdd));
            UpdateTotalTasks();
        }

        #endregion

        #region SaveTaskCommand 

        private RelayCommand _saveTaskCommand;

        public RelayCommand SaveTaskCommand
        {
            get { return _saveTaskCommand ?? (_saveTaskCommand = new RelayCommand(SaveTask, SaveTaskCommandCanExecute)); }
        }

        private void SaveTask(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if (type == "Task")
            {
                TaskLibrary.Add(CopyTask(SelectedTask));
            }
            else if (type == "Template")
            {
                TaskLibrary.Add(CopyTask(SelectedTemplate));
            }
            SelectedTabIndex = 0;
        }

        private bool SaveTaskCommandCanExecute(object obj)
        {
            var type = obj as string;
            if (type == null) return false;

            if (type == "Task")
            {
                return (SelectedTask != null && !SelectedTask.Deprecated && !SelectedTask.MarkedForCompletion);
            }
            else if (type == "Template")
            {
                return (SelectedTemplate != null);
            }
            return false;
        }

        #endregion

        #region NameSorting

        private bool ascendingName = true;
        private RelayCommand _sortTasksByNameCommand;

        public RelayCommand SortTasksByNameCommand
        {
            get { return _sortTasksByNameCommand ?? (_sortTasksByNameCommand = new RelayCommand(SortTasksByName, SortTasksCommandCanExecute)); }
        }

        private bool SortTasksCommandCanExecute(object obj)
        {
            var type = obj as string;
            if (type == "Task")
            {
                return (SelectedTaskList.Count > 0);
            }
            else if (type == "Template")
            {
                return (TaskLibrary.Count > 0);
            }
            return false;
        }
        public void SortTasksByName(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if (ascendingName)
            {
                if (type == "Task")
                {
                    SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.TaskName));
                } 
                else if (type == "Template")
                {
                    TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderBy(task => task.TaskName));
                }
            }
            else
            {
                if (type == "Task")
                {
                    SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.TaskName));
                }
                else if (type == "Template")
                {
                    TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderByDescending(task => task.TaskName));
                }
            }
            ascendingName = !ascendingName;
        }

        #endregion
        #region DifficultySorting

        private bool ascendingDiff = true;
        private RelayCommand _sortTasksByDifficultyCommand;

        public RelayCommand SortTasksByDifficultyCommand
        {
            get { return _sortTasksByDifficultyCommand ?? (_sortTasksByDifficultyCommand = new RelayCommand(SortTasksByDifficulty, SortTasksCommandCanExecute)); }
        }

        public void SortTasksByDifficulty(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if (ascendingDiff)
            {
                if (type == "Task")
                {
                    SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.TaskDifficulty));
                }
                else if (type == "Template")
                {
                    TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderBy(task => task.TaskDifficulty));
                }
            }
            else
            {
                if (type == "Task")
                {
                    SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.TaskDifficulty));
                }
                else if (type == "Template")
                {
                    TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderByDescending(task => task.TaskDifficulty));
                }
            }
            ascendingDiff = !ascendingDiff;
        }
        
        #endregion
        #region CompletionSorting

        private bool ascendingComp = false;
        private RelayCommand _sortTasksByCompletionCommand;

        public RelayCommand SortTasksByCompletionCommand
        {
            get { return _sortTasksByCompletionCommand ?? (_sortTasksByCompletionCommand = new RelayCommand(SortTasksByCompletion, SortTasksCommandCanExecute)); }
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
            get { return _sortTasksByTimeCommand ?? (_sortTasksByTimeCommand = new RelayCommand(SortTasksByTime, SortTasksCommandCanExecute)); }
        }

        public void SortTasksByTime(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if (ascendingTime)
            {
                if (type == "Task")
                {
                    SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => task.NotificationTime));
                }
                else if (type == "Template")
                {
                    TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderBy(task => task.NotificationTime));
                }
            }
            else
            {
                if (type == "Task")
                {
                    SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderByDescending(task => task.NotificationTime));
                }
                else if (type == "Template")
                {
                    TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderByDescending(task => task.NotificationTime));
                }
            }
            ascendingTime = !ascendingTime;
        }

        #endregion  
        #region RandomSorting   

        private RelayCommand _sortTasksRandomlyCommand;

        public RelayCommand SortTasksRandomlyCommand
        {
            get { return _sortTasksRandomlyCommand ?? (_sortTasksRandomlyCommand = new RelayCommand(SortTasksRandomly, SortTasksCommandCanExecute)); }
        }
        public void SortTasksRandomly(object obj)
        {
            var type = obj as string;
            if (type == null) return;

            if (type == "Task")
            {
                SelectedTaskList = new ObservableCollection<TaskViewModel>(SelectedTaskList.OrderBy(task => Guid.NewGuid()));
            }
            else if (type == "Template")
            {
                TaskLibrary = new ObservableCollection<TaskViewModel>(TaskLibrary.OrderBy(task => Guid.NewGuid()));
            }
        }

        #endregion
        #region SaveAndLoad

        private int _completedTasks;

        public int CompletedTasks
        {
            get => _completedTasks;
            set => SetProperty(ref _completedTasks, value);
        }

        private int _totalTasks;

        public int TotalTasks
        {
            get => _totalTasks;
            set => SetProperty(ref _totalTasks, value);
        }

        private string _completionString; 

        public string CompletionString
        {
            get => _completionString;
            set => SetProperty(ref _completionString, value);
        }

        private void UpdateTotalTasks()
        {
            if (SelectedTaskList == null) {
                HasTasks = false;
                return;
            }

            TotalTasks = SelectedTaskList.Count;
            HasTasks = TotalTasks > 0;
            UpdateCompletionString();
        }

        private bool _hasTasks;

        public bool HasTasks
        {
            get => _hasTasks;
            set => SetProperty(ref _hasTasks, value);
        }


        public void UpdateCompletionString()
        {
            if (SelectedTaskList == null || SelectedTaskList.Count == 0)
            {
                CompletionString = LocalizedText.NoTasksText;
                return;
            }

            CompletionString = LocalizedText.CompletedTasksText + CompletedTasks + "/" + TotalTasks;
        }

        public void CountCompletedTasks()
        {
            if (SelectedTaskList == null) return;

            int c = 0;

            foreach (TaskViewModel task in SelectedTaskList)
            {
                if (task.MarkedForCompletion)
                {
                    c++;
                }
            }

            CompletedTasks = c;

            UpdateCompletionString();
        }

        private void LoadTaskDictionary()
        {
            Dictionary<DateOnly, ObservableCollection<TaskModel>> loadedDictionary = ModelDataLoader.LoadTaskDictionary();

            foreach (var pair in loadedDictionary)
            {
                DateOnly date = pair.Key;
                ObservableCollection<TaskModel> taskModelList = pair.Value;
                ObservableCollection<TaskViewModel> taskViewModelList = new ObservableCollection<TaskViewModel>();

                foreach (TaskModel taskModel in taskModelList)
                {
                    TaskViewModel taskViewModel = new TaskViewModel(this, taskModel);
                    taskViewModelList.Add(taskViewModel);
                }
                DateToTaskListDictionary[date] = taskViewModelList;
            }
        }

        private void SaveTaskDictionary()
        {
            Dictionary<DateOnly, ObservableCollection<TaskModel>> dictionaryToSave = new Dictionary<DateOnly, ObservableCollection<TaskModel>>();

            foreach (var pair in DateToTaskListDictionary)
            {
                DateOnly date = pair.Key;
                ObservableCollection<TaskViewModel> taskList = pair.Value;

                ObservableCollection<TaskModel> taskModelList = new ObservableCollection<TaskModel>();
                foreach (TaskViewModel taskViewModel in taskList)
                {
                    TaskModel taskModel = new TaskModel();
                    taskModel.SetAttributes(taskViewModel);
                    taskModelList.Add(taskModel);
                }
                dictionaryToSave.Add(date, taskModelList);
            }

            ModelDataLoader.SaveTaskDictionary(dictionaryToSave);

        }

        private void LoadMainModel()
        {
            MainModel mainModel = ModelDataLoader.LoadMainModel();

            FirstDate = mainModel.FirstDate.ToDateTime(new TimeOnly());
            SelectedDateTime = mainModel.SelectedDate.ToDateTime(new TimeOnly());
            TotalPoints = mainModel.TotalPoints;
        }

        private void SaveMainModel()
        {
            MainModel mainModelToSave = new MainModel();
            mainModelToSave.SetAttributes(this);
            ModelDataLoader.SaveMainModel(mainModelToSave);
        }

        private void LoadTaskLibrary()
        {
            TaskLibrary = new ObservableCollection<TaskViewModel>();
            ObservableCollection<TaskModel> loadedTaskLibrary = ModelDataLoader.LoadTaskLibrary();

            foreach (TaskModel taskModel in loadedTaskLibrary) {
                TaskViewModel taskViewModel = new TaskViewModel(this, taskModel);
                TaskLibrary.Add(taskViewModel);
            }
        }

        private void SaveTaskLibrary()
        {
            ObservableCollection<TaskModel> taskLibraryToSave = new ObservableCollection<TaskModel>();

            foreach (TaskViewModel taskViewModel in TaskLibrary)
            {
                TaskModel taskModel = new TaskModel();
                taskModel.SetAttributes(taskViewModel);
                taskLibraryToSave.Add(taskModel);
            }

            ModelDataLoader.SaveTaskLibrary(taskLibraryToSave);
        }

        #endregion

        #region Timer

        private bool _dayMatch = true;

        public bool DayMatch
        {
            get => _dayMatch;
            set {
                SetProperty(ref _dayMatch, value);
                DayMisMatch = !value;
            } 
        }

        private bool _dayMisMatch = false;

        public bool DayMisMatch
        {
            get => _dayMisMatch;
            set => SetProperty(ref _dayMisMatch, value);
        }


        private bool _isPreparationTime;

        public bool IsPreparationTime
        {
            get => _isPreparationTime;
            set => SetProperty(ref _isPreparationTime, value);
        }

        private Timer _timer; 

        private DateTime _currentDateTime;

        public DateTime CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                SetProperty(ref _currentDateTime, value);
                CurrentTimeString = _currentDateTime.ToString("dd.MM.yyy - HH:mm");
                DayMatch = SelectedDateTime.Date == CurrentDateTime.Date;
                TrySendingNotifications(TimeOnly.FromDateTime(value));
            }
        }

        private string _currentTimeString;

        public String CurrentTimeString
        {
            get {
                return _currentTimeString;
            } 
            set {
                string timeString = value;
                timeString += " - ";

                TimeOnly timeOnly = TimeOnly.FromDateTime(CurrentDateTime);

                if (timeOnly < UserSettings.ProductivityStartTime)
                {
                    timeString += Translator.TranslateToCzech("Preparation Part of the Day");
                    IsPreparationTime = true;
                }
                else if (timeOnly >= UserSettings.ProductivityStartTime && timeOnly < UserSettings.ProductivityEndTime)
                {
                    timeString += Translator.TranslateToCzech("Productive Part of the Day");
                    IsPreparationTime = false;
                }
                else 
                {
                    timeString += Translator.TranslateToCzech("Resting Part of the Day");
                    IsPreparationTime = false;
                }

                MarkTasksAsDeprecated();
                SetProperty(ref _currentTimeString, timeString);
            }
        }

        private bool CheckDeprecationCondition()
        {
            bool deprecated = SelectedDateTime.Date < CurrentDateTime.Date ||
                (SelectedDateTime.Date == CurrentDateTime.Date && TimeOnly.FromDateTime(CurrentDateTime) >= UserSettings.ProductivityEndTime);
            return deprecated;
        }

        private void MarkTasksAsDeprecated()
        {
            // Getting the current date
            DateOnly selectedDateOnly = DateOnly.FromDateTime(SelectedDateTime);
            bool deprecated = CheckDeprecationCondition();

            ObservableCollection<TaskViewModel> presentTaskList = DateToTaskListDictionary[selectedDateOnly];
            if(presentTaskList == null) return;

            foreach (TaskViewModel task in presentTaskList)
            {
                task.Deprecated = deprecated;
            }
        }

        #endregion

        #region TabSelection    

        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        #endregion

        #region TaskNotifications

        private void TrySendingNotifications(TimeOnly timeOnly)
        {
            foreach (TaskViewModel task in SelectedTaskList)
            {
                task.TrySendNotification(timeOnly);
            }
        }

        #endregion

        #region Constructor and Close

        public MainViewModel()
        {
            // Data Loader Instantiation
            ModelDataLoader = new ModelDataLoader(this);

            // Load User Settings 
            UserSettings = new UserSettingsViewModel(this);
            LocalizedText = new LocalizedText(UserSettings.CurrentLanguage);

            // Load Tasks 
            LoadTaskDictionary();

            // Load User Data (eg xp, level and date)
            LoadMainModel(); 

            // Load Task Library
            LoadTaskLibrary();


            // "MainLoop"
            Timer _timer = new Timer(this);
            MarkTasksAsDeprecated();
        }

        public void ExitApplication()
        {
            // Get the Application's current MainWindow
            var mainWindow = Application.Current.MainWindow;

            // Check if MainWindow exists and close it
            if (mainWindow != null)
            {
                mainWindow.Close();
            }
        }

        public void OnWindowClosing()
        {
            if (ModelDataLoader.SaveDeleted) return;

            ModelDataLoader.SaveUserSettings(UserSettings.GetUserSettingsToSave());
            SaveTaskDictionary();
            SaveMainModel();
            SaveTaskLibrary();
        }

        #endregion
    }
}
