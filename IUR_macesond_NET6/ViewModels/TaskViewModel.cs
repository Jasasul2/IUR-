using IUR_macesond_NET6.Support;
using System;
using System.Collections.Generic;
using System.Linq;
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


        private Dictionary<Difficulty, int> DifficultyToExp = new Dictionary<Difficulty, int>()
        {
            { Difficulty.Easy, 3 },
            { Difficulty.Medium, 5 },
            { Difficulty.Hard, 10 }
        };

        #region NotificationTimesAttributes

        TimeOnly _notificationTime1 = new TimeOnly();
        TimeOnly _notificationTime2 = new TimeOnly();
        TimeOnly _notificationTime3 = new TimeOnly();

        public TimeOnly NotificationTime1
        {
            get => _notificationTime1;
            set => SetProperty(ref _notificationTime1, value);
        }
        public TimeOnly NotificationTime2
        {
            get => _notificationTime2;
            set => SetProperty(ref _notificationTime2, value);
        }

        public TimeOnly NotificationTime3
        {
            get => _notificationTime3;
            set => SetProperty(ref _notificationTime3, value);
        }

        public string NotificationTime1StringHour
        {
            get => NotificationTime1.Hour.ToString();
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    NotificationTime1 = new TimeOnly(hour, NotificationTime1.Minute);
                }
            }
        }
 

        public string NotificationTime1StringMinute 
        {
            get => NotificationTime1.Minute.ToString();
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    NotificationTime1 = new TimeOnly(NotificationTime1.Hour, minute);
                }
            }
        }

        public string NotificationTime2StringHour
        {
            get => NotificationTime2.Hour.ToString();
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    NotificationTime2 = new TimeOnly(hour, NotificationTime2.Minute);
                }
            }
        }

        public string NotificiationTime2StringMinute
        {
            get => NotificationTime2.Minute.ToString();
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    NotificationTime2 = new TimeOnly(NotificationTime2.Hour, minute);
                }
            }
        }

        public string NotificationTime3StringHour
        {
            get => NotificationTime3.Hour.ToString();
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    NotificationTime3 = new TimeOnly(hour, NotificationTime3.Minute);
                }
            }
        }

        public string NotificationTime3StringMinute
        {
            get => NotificationTime3.Minute.ToString();
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    NotificationTime3 = new TimeOnly(NotificationTime3.Hour, minute);
                }
            }
        }
        #endregion

        #region OtherAttributes

        private string _taskName;

        private Difficulty _taskDifficulty;

        private TimeOnly[] _taskTimes;

        private string _taskNote;



        public string TaskName
        {
            get => _taskName;
            set => SetProperty(ref _taskName, value);
        }

        public Difficulty TaskDifficulty
        {
            get => _taskDifficulty;
            set => SetProperty(ref _taskDifficulty, value);
        }

        public TimeOnly[] TaskTimes
        {
            get => _taskTimes;
            set => SetProperty(ref _taskTimes, value);
        }

        public string TaskNote
        {
            get => _taskNote;
            set => SetProperty(ref _taskNote, value);
        }
        #endregion

        #region TaskCompletion

        private bool _completed;

        public bool Completed
        {
            get => _completed;
            set => SetProperty(ref _completed, value);
        }

        public void Complete()
        {
            if (Completed) return; 

            Completed = true;
            _mainViewModelReference.AddEXP(DifficultyToExp[TaskDifficulty]);
        }

        #endregion

        public void ResetAttributes()
        {
            TaskName = "";
            TaskDifficulty = Difficulty.Easy;
            TaskTimes = new TimeOnly[3];
            TaskNote = "";
        }

        public TaskViewModel(MainViewModel mainViewModelReference)
        {
            _mainViewModelReference = mainViewModelReference;

            Completed = false;
            ResetAttributes();
        }
    }
}
