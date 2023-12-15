﻿using IUR_macesond.Support;
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
        #region LevelProperties
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
        #endregion

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

        DateTime _productivityStartTime = new DateTime();
        DateTime _productivityEndTime = new DateTime();

        public DateTime ProductivityStartTime
        {
            get => _productivityStartTime;
            set => SetProperty(ref _productivityStartTime, value);
        }

        public DateTime ProductivityEndTime
        {
            get => _productivityEndTime;
            set => SetProperty(ref _productivityEndTime, value);
        }

        #endregion

        public UserSettingsViewModel()
        {
            CurrentLevel = 5;
            CurrentXP = 32;
            NextLevelXP = 100;
            SimplifiedMode = false;
            CurrentLanguage = Language.English;
            CurrentNotificationType = NotificationType.Sound;
        }
    }
}