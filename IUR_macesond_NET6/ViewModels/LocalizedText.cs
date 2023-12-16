using IUR_macesond_NET6.Support;
using IUR_macesond_NET6.Models;
using IUR_macesond_NET6.ValidationRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUR_macesond_NET6.ViewModels
{
    class LocalizedText : ViewModelBase
    {
        #region SettingsLabels

        private string _simplifiedModeLabel = "Simplified mode:";
        private string _productivePartOfTheDayStartLabel = "Productive part of the day start:";
        private string _productivePartOfTheDayEndLabel = "Productive part of the day end:";
        private string _languageLabel = "Language:";
        private string _notificationTypeLabel = "Notification type:";


        public string SimplifiedModeLabel
        {
            get => _simplifiedModeLabel;
            set => SetProperty(ref _simplifiedModeLabel, value);
        }

        public string ProductivePartOfTheDayStartLabel
        {
            get => _productivePartOfTheDayStartLabel;
            set => SetProperty(ref _productivePartOfTheDayStartLabel, value);
        }

        public string ProductivePartOfTheDayEndLabel
        {
            get => _productivePartOfTheDayEndLabel;
            set => SetProperty(ref _productivePartOfTheDayEndLabel, value);
        }

        public string LanguageLabel
        {
            get => _languageLabel;
            set => SetProperty(ref _languageLabel, value);
        }

        public string NotificationTypeLabel
        {
            get => _notificationTypeLabel;
            set => SetProperty(ref _notificationTypeLabel, value);
        }

        #endregion


        #region ButtonLabels

        private string _taskLibraryButton = "Task Library";
        private string _productivityButton = "Productivity";
        private string _settingsButton = "Settings";
        private string _previousButton = "Previous";
        private string _nextButtonButton = "Next";
        private string _alphabetButton = "Alphabet";
        private string _difficultyButton = "Difficulty";
        private string _randomButton = "Random";

        public string TaskLibraryButton
        {
            get => _taskLibraryButton;
            set => SetProperty(ref _taskLibraryButton, value);
        }

        public string ProductivityButton
        {
            get => _productivityButton;
            set => SetProperty(ref _productivityButton, value);
        }

        public string SettingsButton
        {
            get => _settingsButton;
            set => SetProperty(ref _settingsButton, value);
        }

        public string PreviousButton
        {
            get => _previousButton;
            set => SetProperty(ref _previousButton, value);
        }

        public string NextButton
        {
            get => _nextButtonButton;
            set => SetProperty(ref _nextButtonButton, value);
        }

        public string AlphabetButton
        {
            get => _alphabetButton;
            set => SetProperty(ref _alphabetButton, value);
        }

        public string DifficultyButton
        {
            get => _difficultyButton;
            set => SetProperty(ref _difficultyButton, value);
        }

        public string RandomButton
        {
            get => _randomButton;
            set => SetProperty(ref _randomButton, value);
        }
        #endregion

        public void SetLanguage(UserSettingsViewModel.Language newLanguage)
        {
            switch (newLanguage)
            {
                case UserSettingsViewModel.Language.English:
                    SimplifiedModeLabel = "Simplified mode:";
                    ProductivePartOfTheDayStartLabel = "Productive part of the day start:";
                    ProductivePartOfTheDayEndLabel = "Productive part of the day end:";
                    LanguageLabel = "Language:";
                    NotificationTypeLabel = "Notification type:";

                    TaskLibraryButton = "Task Library";
                    ProductivityButton = "Productivity";
                    SettingsButton = "Settings";
                    PreviousButton = "Previous";
                    NextButton = "Next";
                    AlphabetButton = "Alphabet";
                    DifficultyButton = "Difficulty";
                    RandomButton = "Random";
                    break;

                case UserSettingsViewModel.Language.Czech:
                    SimplifiedModeLabel = "Zjednodušený režim:";
                    ProductivePartOfTheDayStartLabel = "Začátek produktivní části dne:";
                    ProductivePartOfTheDayEndLabel = "Konec produktivní části dne:";
                    LanguageLabel = "Jazyk:";
                    NotificationTypeLabel = "Typ upozornění:";

                    TaskLibraryButton = "Knihovna";
                    ProductivityButton = "Produktivita";
                    SettingsButton = "Nastavení";
                    PreviousButton = "Předchozí";
                    NextButton = "Další";
                    AlphabetButton = "Abeceda";
                    DifficultyButton = "Obtížnost";
                    RandomButton = "Náhodně";
                    break;
            }
        }

        public LocalizedText(UserSettingsViewModel.Language defaultLanguage)
        {
            SetLanguage(defaultLanguage);
        }
    }
}
