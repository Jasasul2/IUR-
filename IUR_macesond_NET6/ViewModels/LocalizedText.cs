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

        private string _deleteSaveFilesLabel = "Delete Save Files and Quit";
        private string _productivePartOfTheDayStartLabel = "Productive part of the day start:";
        private string _productivePartOfTheDayEndLabel = "Productive part of the day end:";
        private string _languageLabel = "Language:";
        private string _soundNotificationLabel = "Notification sounds:";


        public string DeleteSaveFilesLabel
        {
            get => _deleteSaveFilesLabel;
            set => SetProperty(ref _deleteSaveFilesLabel, value);
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

        public string NotificationSounds
        {
            get => _soundNotificationLabel;
            set => SetProperty(ref _soundNotificationLabel, value);
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
        private string _completedButton = "Completed";
        private string _timeButton = "Time";
        private string _randomButton = "Random";
        private string _addNewTaskButton = "Add New Task";
        private string _addRandomTaskButton = "Add Random Task from Task Library";
        private string _saveButton = "Save";
        private string _resetButton = "Reset";
        private string _deleteButton = "Delete";
        private string _addTaskTemplateButton = "Add New Task Template";    

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

        public string CompletedButton
        {
            get => _completedButton;
            set => SetProperty(ref _completedButton, value);
        }

        public string TimeButton
        {
            get => _timeButton;
            set => SetProperty(ref _timeButton, value);
        }

        public string RandomButton
        {
            get => _randomButton;
            set => SetProperty(ref _randomButton, value);
        }

        public string AddNewTaskButton
        {
            get => _addNewTaskButton;
            set => SetProperty(ref _addNewTaskButton, value);
        }

        public string AddRandomTaskButton
        {
            get => _addRandomTaskButton;
            set => SetProperty(ref _addRandomTaskButton, value);
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

        public string DeleteButton
        {
            get => _deleteButton;
            set => SetProperty(ref _deleteButton, value);
        }

        public string AddTaskTemplateButton
        {
            get => _addTaskTemplateButton;
            set => SetProperty(ref _addTaskTemplateButton, value);
        }

        #endregion


        #region TaskEditorLabels

        private string _invisibleTaskEditorLabel = "Select a task to Edit";

        private string _taskEditorTitle = "Task Editor";
        private string _taskTemplateEditorTitle = "Task Template Editor";

        private string _taskEditorNameLabel = "Task name:";
        private string _taskEditorTemplateNameLabel = "Task template name:";

        private string _taskEditorDifficultyLabel = "Task difficulty:";
        private string _taskEditorTemplateDifficultyLabel = "Task template difficulty:";

        private string _taskEditorNotificationTimesLabel = "Task notification time:";
        private string _taskEditorTemplateNotificationTimesLabel = "Task template notification time:";


        bool _taskEditorNotificationEnabled = false;
        private string _taskEditorNotificationEnabledLabel = "Enabled";
        private string _taskEditorNotificationDisabledLabel = "Disabled";

        private string _taskEditorNoteLabel = "Task note:";
        private string _taskEditorTemplateNoteLabel = "Task template note:";

        private string _taskEditorPlaceholderName = "Enter a task name";
        private string _taskEditorPlaceholderNote = "Enter a task note";

        private string _taskEditorTemplatePlaceholderName = "Enter a task template name";
        private string _taskeditorTemplatePlaceholderNote = "Enter a task template note";

        public string InvisibleTaskEditorLabel
        {
            get => _invisibleTaskEditorLabel;
            set => SetProperty(ref _invisibleTaskEditorLabel, value);
        }

        public string TaskEditorTitle
        {
            get => _taskEditorTitle;
            set => SetProperty(ref _taskEditorTitle, value);
        }

        public string TaskTemplateEditorTitle
        {
            get => _taskTemplateEditorTitle;
            set => SetProperty(ref _taskTemplateEditorTitle, value);
        }

        public string TaskEditorNameLabel
        {
            get => _taskEditorNameLabel;
            set => SetProperty(ref _taskEditorNameLabel, value);
        }

        public string TaskEditorTemplateNameLabel
        {
            get => _taskEditorTemplateNameLabel;
            set => SetProperty(ref _taskEditorTemplateNameLabel, value);
        }

        public string TaskEditorDifficultyLabel
        {
            get => _taskEditorDifficultyLabel;
            set => SetProperty(ref _taskEditorDifficultyLabel, value);
        }

        public string TaskEditorTemplateDifficultyLabel
        {
            get => _taskEditorTemplateDifficultyLabel;
            set => SetProperty(ref _taskEditorTemplateDifficultyLabel, value);
        }

        public string TaskEditorNotificationTimesLabel
        {
            get => _taskEditorNotificationTimesLabel;
            set => SetProperty(ref _taskEditorNotificationTimesLabel, value);
        }

        public string TaskEditorTemplateNotificationTimesLabel
        {
            get => _taskEditorTemplateNotificationTimesLabel;
            set => SetProperty(ref _taskEditorTemplateNotificationTimesLabel, value);
        }


        public string TaskEditorNotificationEnabledLabel
        {
            get
            {
                if (_taskEditorNotificationEnabled)
                {
                    return _taskEditorNotificationEnabledLabel;
                }
                else
                {
                    return _taskEditorNotificationDisabledLabel;
                }   
            }
            set => SetProperty(ref _taskEditorNotificationEnabledLabel, value);
        }

        public string TaskEditorNoteLabel
        {
            get => _taskEditorNoteLabel;
            set => SetProperty(ref _taskEditorNoteLabel, value);
        }

        public string TaskEditorTemplateNoteLabel
        {
            get => _taskEditorTemplateNoteLabel;
            set => SetProperty(ref _taskEditorTemplateNoteLabel, value);
        }

        public string TaskEditorPlaceholderName
        {
            get => _taskEditorPlaceholderName;
            set => SetProperty(ref _taskEditorPlaceholderName, value);
        }

        public string TaskEditorPlaceholderNote
        {
            get => _taskEditorPlaceholderNote;
            set => SetProperty(ref _taskEditorPlaceholderNote, value);
        }

        public string TaskEditorTemplatePlaceholderName
        {
            get => _taskEditorTemplatePlaceholderName;
            set => SetProperty(ref _taskEditorTemplatePlaceholderName, value);
        }

        public string TaskEditorTemplatePlaceholderNote
        {
            get => _taskeditorTemplatePlaceholderNote;
            set => SetProperty(ref _taskeditorTemplatePlaceholderNote, value);
        }

        #endregion

        #region OtherLabels

        private string _sortByLabel = "Sort by:";
        private string _levelLabel = "Level";
        private string _useLabel = "Use";

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

        public string UseLabel
        {
            get => _useLabel;
            set => SetProperty(ref _useLabel, value);
        }

        #endregion

        public void SetLanguage(UserSettingsViewModel.Language newLanguage)
        {
            switch (newLanguage)
            {
                case UserSettingsViewModel.Language.EN:
                    DeleteSaveFilesLabel = "Delete Save Files and Quit";
                    ProductivePartOfTheDayStartLabel = "Productive part of the day start:";
                    ProductivePartOfTheDayEndLabel = "Productive part of the day end:";
                    LanguageLabel = "Language:";
                    NotificationSounds = "Notification sounds:";

                    TaskLibraryButton = "Task Library";
                    ProductivityButton = "Productivity";
                    SettingsButton = "Settings";
                    PreviousButton = "Previous";
                    NextButton = "Next";
                    AlphabetButton = "Alphabet";
                    DifficultyButton = "Difficulty";
                    CompletedButton = "Completed";
                    TimeButton = "Time";
                    RandomButton = "Random";
                    AddNewTaskButton = "Add New Task";
                    AddRandomTaskButton = "Add Random Task from Task Library";
                    SaveButton = "Save";
                    ResetButton = "Reset";
                    DeleteButton = "Delete";

                    AddTaskTemplateButton = "Add New Task Template";

                    InvisibleTaskEditorLabel = "Select a task to Edit";

                    TaskEditorTitle = "Task Editor";
                    TaskTemplateEditorTitle = "Task Template Editor";
                    TaskEditorNameLabel = "Task name:";
                    TaskEditorTemplateNameLabel = "Task template name:";
                    TaskEditorDifficultyLabel = "Task difficulty:";
                    TaskEditorTemplateDifficultyLabel = "Task template difficulty:";
                    TaskEditorNotificationTimesLabel = "Task notification time:";
                    TaskEditorTemplateNotificationTimesLabel = "Task template notification time:";
                    _taskEditorNotificationEnabledLabel = "Enabled";
                    _taskEditorNotificationDisabledLabel = "Disabled";
                    TaskEditorNoteLabel = "Task note:";
                    TaskEditorTemplateNoteLabel = "Task template note:";

                    TaskEditorPlaceholderName = "Enter a task name";
                    TaskEditorPlaceholderNote = "Enter a task note";

                    TaskEditorTemplatePlaceholderName = "Enter a task template name";
                    TaskEditorTemplatePlaceholderNote = "Enter a task template note";

                    SortByLabel = "Sort by:";
                    LevelLabel = "Level";
                    UseLabel = "Use";

                    break;

                case UserSettingsViewModel.Language.CZ:
                    DeleteSaveFilesLabel = "Odstranit data a vypnout";
                    ProductivePartOfTheDayStartLabel = "Začátek produktivní části dne:";
                    ProductivePartOfTheDayEndLabel = "Konec produktivní části dne:";
                    LanguageLabel = "Jazyk:";
                    NotificationSounds = "Zvuk upozornění:";
                    DeleteButton = "Smazat";

                    TaskLibraryButton = "Knihovna";
                    ProductivityButton = "Produktivita";
                    SettingsButton = "Nastavení";
                    PreviousButton = "Předchozí";
                    NextButton = "Další";
                    AlphabetButton = "Abecedy";
                    DifficultyButton = "Obtížnosti";
                    CompletedButton = "Hotovosti";
                    TimeButton = "Času";
                    RandomButton = "Náhodně";
                    AddNewTaskButton = "Přidat nový úkol";
                    AddRandomTaskButton = "Přidat náhodný úkol z knihovny úkolů";

                    SaveButton = "Uložit";
                    ResetButton = "Reset";

                    AddTaskTemplateButton = "Přidat šablonu úkolu";

                    InvisibleTaskEditorLabel = "Vyberte úkol k úpravě";

                    TaskEditorTitle = "Editor úkolů";
                    TaskTemplateEditorTitle = "Editor šablon úkolů";
                    TaskEditorNameLabel = "Název úkolu:";
                    TaskEditorTemplateNameLabel = "Název šablony úkolu:";
                    TaskEditorDifficultyLabel = "Obtížnost úkolu:";
                    TaskEditorTemplateDifficultyLabel = "Obtížnost šablony úkolu:";
                    TaskEditorNotificationTimesLabel = "Čas upozornění:";
                    TaskEditorTemplateNotificationTimesLabel = "Čas upozornění šablony úkolu:";
                    _taskEditorNotificationEnabledLabel = "Zapnuto";
                    _taskEditorNotificationDisabledLabel = "Vypnuto";
                    TaskEditorNoteLabel = "Poznámka k úkolu:";
                    TaskEditorTemplateNoteLabel = "Poznámka k šabloně úkolu:";

                    TaskEditorPlaceholderName = "Zadejte název úkolu";
                    TaskEditorPlaceholderNote = "Zadejte poznámku k úkolu";

                    TaskEditorTemplatePlaceholderName = "Zadejte název šablony úkolu";
                    TaskEditorTemplatePlaceholderNote = "Zadejte poznámku k šabloně úkolu";

                    SortByLabel = "Řadit dle:";
                    LevelLabel = "Úroveň";
                    UseLabel = "Použít";

                    break;
            }

            ToggleTaskEditorNotificationLabel(_taskEditorNotificationEnabled);
        }

        public void ToggleTaskEditorNotificationLabel(bool enabled)
        {
            _taskEditorNotificationEnabled = enabled;
            OnPropertyChanged(nameof(TaskEditorNotificationEnabledLabel));
        }

        public LocalizedText(UserSettingsViewModel.Language defaultLanguage)
        {
            SetLanguage(defaultLanguage);
        }
    }
}
