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
        #region Saving
        public UserSettingsModel GetUserSettingsToSave()
        {
            return new UserSettingsModel()
            {
                CurrentLanguage = CurrentLanguage,
                NotificationSoundsEnabled = NotificationSoundsEnabled,
                ProductivityStartTime = ProductivityStartTime,
                ProductivityEndTime = ProductivityEndTime
            };
        }

        #endregion

        private MainViewModel _mainViewModelReference;
        private void RefreshMainViewModelTime() {
            if (_mainViewModelReference.UserSettings == null) return;
            _mainViewModelReference.CurrentDateTime = DateTime.Now;
        } 

        #region Language

        public enum Language
        {
            CZ,
            EN
        }

        public Array LanguageArray
        {
            get { return Enum.GetValues(typeof(Language)); }
        }

        private Language _currentLanguage;

        public Language CurrentLanguage
        {
            get => _currentLanguage;
            set {  
                SetProperty(ref _currentLanguage, value);
                CurrentLanguageString = value.ToString();

                Translator.CurrentLanguage = value;

                if (_mainViewModelReference.LocalizedText!= null)
                {
                    _mainViewModelReference.LocalizedText.SetLanguage(value);
                }
                RefreshMainViewModelTime();
            }
        }

        private string _currentLanguageString;
        public string CurrentLanguageString
        {
            get => _currentLanguageString;
            set => SetProperty(ref _currentLanguageString, value);
        }

        #endregion

        #region Notifications
        
        private bool _notificationSoundsEnabled;

        public bool NotificationSoundsEnabled
        {
            get => _notificationSoundsEnabled;
            set => SetProperty(ref _notificationSoundsEnabled, value);
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


        private string _productivityStartTimeStringHour;
        private string _productivityStartTimeStringMinute;
        private string _productivityEndTimeStringHour;
        private string _productivityEndTimeStringMinute;

        public string ProductivityStartTimeStringHour
        {
            get => _productivityStartTimeStringHour;
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    SetProperty(ref _productivityStartTimeStringHour, value);
                    ProductivityStartTime = new TimeOnly(hour, ProductivityStartTime.Minute);
                    RefreshMainViewModelTime();
                }
            }
        }

        public string ProductivityStartTimeStringMinute
        {
            get => _productivityStartTimeStringMinute;
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    SetProperty(ref _productivityStartTimeStringMinute, value);
                    ProductivityStartTime = new TimeOnly(ProductivityStartTime.Hour, minute);
                    RefreshMainViewModelTime();
                }
            }
        }   

        public string ProductivityEndTimeStringHour
        {
            get => _productivityEndTimeStringHour;
            set
            {
                if (int.TryParse(value, out int hour))
                {
                    SetProperty(ref _productivityEndTimeStringHour, value);
                    ProductivityEndTime = new TimeOnly(hour, ProductivityEndTime.Minute);
                    RefreshMainViewModelTime();
                }
            }
        }

        public string ProductivityEndTimeStringMinute
        {
            get => _productivityEndTimeStringMinute;
            set
            {
                if (int.TryParse(value, out int minute))
                {
                    SetProperty(ref _productivityEndTimeStringMinute, value);
                    ProductivityEndTime = new TimeOnly(ProductivityEndTime.Hour, minute);
                    RefreshMainViewModelTime();
                }
            }
        }

        #endregion

        public UserSettingsViewModel(MainViewModel mainViewModelReference)
        {
            _mainViewModelReference = mainViewModelReference;

            // Setting up the comboBox collections 

            UserSettingsModel savedUserSettings = mainViewModelReference.ModelDataLoader.LoadUserSettings();

            ProductivityStartTime = savedUserSettings.ProductivityStartTime;
            ProductivityEndTime = savedUserSettings.ProductivityEndTime;

            ProductivityStartTimeStringHour = ProductivityStartTime.Hour.ToString();
            ProductivityStartTimeStringMinute = ProductivityStartTime.Minute.ToString();
            ProductivityEndTimeStringHour = ProductivityEndTime.Hour.ToString();
            ProductivityEndTimeStringMinute = ProductivityEndTime.Minute.ToString();



            CurrentLanguage = savedUserSettings.CurrentLanguage;
            NotificationSoundsEnabled = savedUserSettings.NotificationSoundsEnabled;
        }
    }
}
