using IUR_macesond.Support;
using IUR_macesond.Models;
using IUR_macesond.ValidationRules;
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


namespace IUR_macesond.ViewModels
{
    internal class UserSettingsViewModel : ViewModelBase
    {
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

        public UserSettingsViewModel()
        {
            CurrentLevel = 0;
            CurrentXP = 32;
            NextLevelXP = 100;
        }
    }
}
