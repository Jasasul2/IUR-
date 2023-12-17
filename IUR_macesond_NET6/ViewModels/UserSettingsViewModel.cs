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
using IUR_macesond_NET6.Converters;


namespace IUR_macesond_NET6.ViewModels
{
    internal class UserSettingsViewModel : ViewModelBase
    {

        private MainViewModel _mainViewModelReference;

        #region SimplifiedMode
        private bool _simplifiedMode;

        public bool SimplifiedMode
        {
            get => _simplifiedMode;
            set => SetProperty(ref _simplifiedMode, value);
        }

        #endregion

        #region Language

        public enum Language
        {
            Czech,
            English
        }

        private ObservableCollection<string> _languageComboBoxCollection;
        public ObservableCollection<string> LanguageComboBoxCollection
        {
            get { return _languageComboBoxCollection; }
            set
            {
                _languageComboBoxCollection = value;
                OnPropertyChanged(nameof(_languageComboBoxCollection));
            }
        }

        public string[] LanguageArray
        {
            get { return Enum.GetNames(typeof(Language)); }
        }

        private Language _currentLanguage;

        public Language CurrentLanguage
        {
            get => _currentLanguage;
            set {  
                SetProperty(ref _currentLanguage, value);
                CurrentLanguageString = value.ToString();

                // This is some serious spaghetti code, but it works
                Translator.CurrentLanguage = value;
                LanguageComboBoxCollection = Translator.translateCollection(LanguageComboBoxCollection);
                NotificationComboBoxCollection = Translator.translateCollection(NotificationComboBoxCollection);

                if (_mainViewModelReference.LocalizedText!= null)
                {
                    _mainViewModelReference.LocalizedText.SetLanguage(value);
                }
            }
        }

        private string _currentLanguageString;
        public string CurrentLanguageString
        {
            get => _currentLanguageString;
            set
            {
                SetProperty(ref _currentLanguageString, value);
            }
        }

        #endregion

        #region Notifications
        public enum NotificationType
        {
            Sound,
            Text,
            Both,
            None
        }

        private ObservableCollection<string> _notificationComboBoxCollection;

        public ObservableCollection<string> NotificationComboBoxCollection
        {
            get { return _notificationComboBoxCollection; }
            set
            {
                _notificationComboBoxCollection = value;
                OnPropertyChanged(nameof(_notificationComboBoxCollection));
            }
        }

        public string[] NotificationTypeArray
        {
            get { return Enum.GetNames(typeof(NotificationType)); }
        }

        private NotificationType _currentNotificationType;

        public NotificationType CurrentNotificationType
        {
            get => _currentNotificationType;
            set => SetProperty(ref _currentNotificationType, value);
        }
        #endregion

        #region Time

        TimeOnly _productivityStartTime = new TimeOnly();
        TimeOnly _productivityEndTime = new TimeOnly();

        public TimeOnly ProductivityStartTime
        {
            get => _productivityStartTime;
            set => SetProperty(ref _productivityStartTime, value);
        }
        public TimeOnly ProductivityEndTime
        {
            get => _productivityEndTime;
            set => SetProperty(ref _productivityEndTime, value);
        }

        public string ProductivityStartTimeStringHour
        {
            get => ProductivityStartTime.Hour.ToString();
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    ProductivityStartTime = new TimeOnly(hour, ProductivityStartTime.Minute);
                }
            }
        }

        public string ProductivityStartTimeStringMinute
        {
            get => ProductivityStartTime.Minute.ToString();
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    ProductivityStartTime = new TimeOnly(ProductivityStartTime.Hour, minute);
                }
            }
        }   

        public string ProductivityEndTimeStringHour
        {
            get => ProductivityEndTime.Hour.ToString();
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    ProductivityEndTime = new TimeOnly(hour, ProductivityEndTime.Minute);
                }
            }
        }

        public string ProductivityEndTimeStringMinute
        {
            get => ProductivityEndTime.Minute.ToString();
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    ProductivityEndTime = new TimeOnly(ProductivityEndTime.Hour, minute);
                }
            }
        }

        #endregion

        public UserSettingsViewModel(MainViewModel mainViewModelReference)
        {
            _mainViewModelReference = mainViewModelReference;

            // Setting up the comboBox collections 
            LanguageComboBoxCollection = new ObservableCollection<string>(LanguageArray);
            NotificationComboBoxCollection = new ObservableCollection<string>(NotificationTypeArray);

            SimplifiedMode = false;
            CurrentLanguage = Language.English ;
            CurrentNotificationType = NotificationType.Sound;
            ProductivityStartTime = new TimeOnly(8, 45);
            ProductivityEndTime = new TimeOnly(22, 30);
        }
    }
}
