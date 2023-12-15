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


namespace IUR_macesond_NET6.ViewModels
{
    internal class UserSettingsViewModel : ViewModelBase
    {

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
        public Array LanguageArray
        {
            get { return Enum.GetValues(typeof(Language)); }
        }

        private Language _currentLanguage;

        public Language CurrentLanguage
        {
            get => _currentLanguage;
            set => SetProperty(ref _currentLanguage, value);
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
        public Array NotificationTypeArray
        {
            get { return Enum.GetValues(typeof(NotificationType)); }
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

        public UserSettingsViewModel()
        {
            SimplifiedMode = false;
            CurrentLanguage = Language.English;
            CurrentNotificationType = NotificationType.Sound;
            ProductivityStartTime = new TimeOnly(8, 45);
            ProductivityEndTime = new TimeOnly(22, 30);
        }
    }
}
