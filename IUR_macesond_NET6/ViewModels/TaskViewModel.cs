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

        private string _taskName;

        private Difficulty _taskDifficulty;

        private TimeOnly[] _taskTimes;

        private string _taskNote;

        private bool _taskCompleted;

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

        public bool TaskCompleted
        {
            get => _taskCompleted;
            set => SetProperty(ref _taskCompleted, value);
        }

        public TaskViewModel(MainViewModel mainViewModelReference)
        {
            _mainViewModelReference = mainViewModelReference;

            TaskName = "Test task name";
            TaskDifficulty = Difficulty.Hard;
            TaskTimes = new TimeOnly[3];
            TaskNote = "Test task note";
            TaskCompleted = false;
        }
    }
}
