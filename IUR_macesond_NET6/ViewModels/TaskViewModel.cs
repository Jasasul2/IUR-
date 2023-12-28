using IUR_macesond_NET6.Models;
using IUR_macesond_NET6.Support;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace IUR_macesond_NET6.ViewModels
{
    // Outside the class so its accesible from XAML
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    internal class TaskViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModelReference;

        public MainViewModel MainViewModelReference
        {
            get => _mainViewModelReference;
            set => SetProperty(ref _mainViewModelReference, value);
        }

        private Dictionary<Difficulty, int> DifficultyToExp = new Dictionary<Difficulty, int>()
        {
            { Difficulty.Easy, 3 },
            { Difficulty.Medium, 5 },
            { Difficulty.Hard, 10 }
        };

        #region NotificationTimesAttributes

        private bool _hasNotificationTime = false;

        // This is a horrible code sorry for that 
        private bool _hasNotificationTimeNegation = true; 

        public bool HasNotificationTime
        {
            get => _hasNotificationTime;
            set {
                SetProperty(ref _hasNotificationTime, value);
                DoesNotHaveNotificationTime = !_hasNotificationTime;
                if (_mainViewModelReference != null)
                {
                    _mainViewModelReference.LocalizedText.ToggleTaskEditorNotificationLabel(value);
                }
            } 
        }

        public bool DoesNotHaveNotificationTime
        {
            get => !_hasNotificationTime;
            set => SetProperty(ref _hasNotificationTimeNegation, value);
        }
        
        TimeOnly _notificationTime = new TimeOnly();
        private int _notificationTimeHours;
        private int _notificationTimeMinutes;

        public TimeOnly NotificationTime
        {
            get => _notificationTime;
            set { 
                SetProperty(ref _notificationTime, value);
                SetProperty(ref _notificationTimeHours, value.Hour);
                SetProperty(ref _notificationTimeMinutes, value.Minute);
            }
        }

        public string NotificationTimeStringHour
        {
            get => _notificationTimeHours.ToString();
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    SetProperty(ref _notificationTimeHours, hour);
                    NotificationTime = new TimeOnly(hour, NotificationTime.Minute);
                }
                //SelectThisTask();
            }
        }
 

        public string NotificationTimeStringMinute 
        {
            get => _notificationTimeMinutes.ToString();
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    SetProperty(ref _notificationTimeMinutes, minute);
                    NotificationTime = new TimeOnly(NotificationTime.Hour, minute);
                }
                //SelectThisTask();
            }
        }

        #endregion

        #region OtherAttributes

        private string _taskName;

        private Difficulty _taskDifficulty;

        private string _taskNote;



        public string TaskName
        {
            get => _taskName;
            set {
                SetProperty(ref _taskName, value);
                //SelectThisTask();
            } 
        }

        public Difficulty TaskDifficulty
        {
            get => _taskDifficulty;
            set
            {
                SetProperty(ref _taskDifficulty, value);
                //SelectThisTask();
            }
        }

        public string TaskNote
        {
            get => _taskNote;
            set
            {
                SetProperty(ref _taskNote, value);
                //SelectThisTask();
            }
        }
        #endregion

        #region TaskCompletion

        // This is bound to a checkbox ... after productivity part ends,
        // Task is completed and EXP is awarded 


        private bool _markedForCompletion;
        private bool _completed = false;

        public bool MarkedForCompletion
        {
            get => _markedForCompletion;
            set { 
                SetProperty(ref _markedForCompletion, value);
                if (value)
                {
                    Complete();
                }
                else 
                {
                    UnComplete();
                }
                //SelectThisTask();
            }
        }

        // When the task is outside the producitivty part, it can no longer be changed
        private bool _deprecated;

        public bool Deprecated
        {
            get => _deprecated;
            set => SetProperty(ref _deprecated, value);

        }

        public void Complete()
        {
            if (_completed) return;
            
            _completed = true;
            _mainViewModelReference.CountCompletedTasks();
            _mainViewModelReference.AddPoints(DifficultyToExp[TaskDifficulty]);
        }

        private void UnComplete()
        {
            if (!_completed) return;

            _completed = false;
            _mainViewModelReference.CountCompletedTasks();
            _mainViewModelReference.AddPoints(-DifficultyToExp[TaskDifficulty]);
        }

        #endregion

        #region TaskDeleteCommand

        private RelayCommand _removeTaskCommand;

        public RelayCommand RemoveTaskCommand
        {
            get { return _removeTaskCommand ?? (_removeTaskCommand = new RelayCommand(RemoveTask, RemoveTaskCommandCanExecute)); }
        }

        private bool RemoveTaskCommandCanExecute(object obj)
        {
            return !Deprecated;
        }

        private void RemoveTask(object obj)
        {
            var param = obj as string;
            if (param == null) return; 

            _mainViewModelReference.DeleteTask(this, param);

        }

        #endregion

        #region UseTaskTemplateCommand  

        private RelayCommand _useTaskTemplateCommand;

        public RelayCommand UseTaskTemplateCommand

        {
            get { return _useTaskTemplateCommand ?? (_useTaskTemplateCommand = new RelayCommand(UseTaskTemplate, UseTaskTemplateCommandCanExecute)); }
        }

        private bool UseTaskTemplateCommandCanExecute(object obj)
        {
            return true; 
        }

        private void UseTaskTemplate(object obj)
        {
            _mainViewModelReference.UseTaskTemplate(this);
        }

        #endregion

        #region TaskNoteVisibilityCommand

        private bool _taskNoteVisibility = false;

        public bool TaskNoteVisibility
        {
            get => _taskNoteVisibility;
            set => SetProperty(ref _taskNoteVisibility, value);
        }

        private RelayCommand _toggleTaskNoteVisibilityCommand;

        public RelayCommand ToggleTaskNoteVisibilityCommand
        {
            get { return _toggleTaskNoteVisibilityCommand ?? (_toggleTaskNoteVisibilityCommand = new RelayCommand(ToggleTaskNoteVisibility, ToggleTaskNoteVisibilityCanExecute)); }
        }

        private bool ToggleTaskNoteVisibilityCanExecute(object obj)
        {
            return true;
        }

        private void ToggleTaskNoteVisibility(object obj)
        {
            TaskNoteVisibility = !TaskNoteVisibility;
        }

        #endregion

        public void SetAttributes(TaskModel taskModel)
        {
            TaskName = taskModel.TaskName;
            HasNotificationTime = taskModel.HasNotificationTime;
            NotificationTime = taskModel.NotificationTime;
            TaskDifficulty = taskModel.TaskDifficulty;
            TaskNote= taskModel.TaskNote;
            MarkedForCompletion = taskModel.MarkedForCompletion;
            TaskNoteVisibility = taskModel.TaskNoteVisibility;
        }

        private RelayCommand _selectThisTaskCommand;

        public RelayCommand SelectThisTaskCommand
        {
            get { return _selectThisTaskCommand ?? (_selectThisTaskCommand = new RelayCommand(SelectThisTask, SelectThisTaskCommandCanExecute)); }
        }

        private bool SelectThisTaskCommandCanExecute(object obj)
        {
            return true;
        }

        private void SelectThisTask(object obj)
        {
            _mainViewModelReference.SelectThisTask(this);
        }

        #region SelectNextDifficultyCommand

        private RelayCommand _selectNextDifficultyCommand; 

        public RelayCommand SelectNextDifficultyCommand
        {
            get { return _selectNextDifficultyCommand ?? (_selectNextDifficultyCommand = new RelayCommand(SelectNextDifficulty, SelectNextDifficultyCommandCanExecute)); }
        }

        private bool SelectNextDifficultyCommandCanExecute(object obj)
        {
            return true;
        }

        private void SelectNextDifficulty(object obj)
        {
            TaskDifficulty = (Difficulty)(((int)TaskDifficulty + 1) % 3);
        }

        #endregion

        public TaskViewModel(MainViewModel mainViewModelReference, TaskModel taskModel)
        {
            _mainViewModelReference = mainViewModelReference;

            MarkedForCompletion = false;
            Deprecated = false;
            SetAttributes(taskModel);
        }
    }
}
