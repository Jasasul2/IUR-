using IUR_macesond_NET6.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUR_macesond_NET6.ViewModels
{
    internal class TaskViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModelReference;

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        private Dictionary<Difficulty, int> DifficultyToExp = new Dictionary<Difficulty, int>()
        {
            { Difficulty.Easy, 3 },
            { Difficulty.Medium, 5 },
            { Difficulty.Hard, 10 }
        };

        public TaskViewModel(MainViewModel mainViewModelReference, string location)
        {
            _mainViewModelReference = mainViewModelReference;
        }
    }
}
