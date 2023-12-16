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
        private string _saveButton = "Save";
        private string _resetButton = "Reset";

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

        public string SaveButton
        {
            get => _saveButton;
            set => SetProperty(ref _saveButton, value);
        }

        public string ResetButton
        {
            get => _resetButton;
            set => SetProperty(ref _resetButton, value);
        }

        #endregion


        #region TaskEditorLabels

        private string _taskEditorTitle = "Task Editor";
        private string _taskEditorNameLabel = "Task name:";
        private string _taskEditorDifficultyLabel = "Task difficulty:";
        private string _taskEditorNotificationTimesLabel = "Task notification times:";
        private string _taskEditorNoteLabel = "Task note:";

        
        public string TaskEditorTitle
        {
            get => _taskEditorTitle;
            set => SetProperty(ref _taskEditorTitle, value);
        }

        public string TaskEditorNameLabel
        {
            get => _taskEditorNameLabel;
            set => SetProperty(ref _taskEditorNameLabel, value);
        }

        public string TaskEditorDifficultyLabel
        {
            get => _taskEditorDifficultyLabel;
            set => SetProperty(ref _taskEditorDifficultyLabel, value);
        }

        public string TaskEditorNotificationTimesLabel
        {
            get => _taskEditorNotificationTimesLabel;
            set => SetProperty(ref _taskEditorNotificationTimesLabel, value);
        }

        public string TaskEditorNoteLabel
        {
            get => _taskEditorNoteLabel;
            set => SetProperty(ref _taskEditorNoteLabel, value);
        }

        #endregion

        #region OtherLabels

        private string _sortByLabel = "Sort by:";
        private string _levelLabel = "Level";

        public string SortByLabel
        {
            get => _sortByLabel;
            set => SetProperty(ref _sortByLabel, value);
        }

        public string LevelLabel
        {
            get => _levelLabel;
            set => SetProperty(ref _levelLabel, value);
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
                    SaveButton = "Save";
                    ResetButton = "Reset";

                    TaskEditorTitle = "Task Editor";
                    TaskEditorNameLabel = "Task name:";
                    TaskEditorDifficultyLabel = "Task difficulty:";
                    TaskEditorNotificationTimesLabel = "Task notification times:";
                    TaskEditorNoteLabel = "Task note:";

                    SortByLabel = "Sort by:";
                    LevelLabel = "Level";

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
                    AlphabetButton = "Abecedy";
                    DifficultyButton = "Obtížnosti";
                    RandomButton = "Náhodně";
                    SaveButton = "Uložit";
                    ResetButton = "Reset";

                    TaskEditorTitle = "Editor úkolů";
                    TaskEditorNameLabel = "Název úkolu:";
                    TaskEditorDifficultyLabel = "Obtížnost úkolu:";
                    TaskEditorNotificationTimesLabel = "Časy upozornění:";
                    TaskEditorNoteLabel = "Poznámka k úkolu:";

                    SortByLabel = "Řadit dle:";
                    LevelLabel = "Úroveň";

                    break;
            }
        }

        public LocalizedText(UserSettingsViewModel.Language defaultLanguage)
        {
            SetLanguage(defaultLanguage);
        }
    }
}
