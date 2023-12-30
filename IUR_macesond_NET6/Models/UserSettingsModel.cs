using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUR_macesond_NET6.ViewModels;

namespace IUR_macesond_NET6.Models
{
    class UserSettingsModel
    {

        public bool NotificationSoundsEnabled { get; set; }
        public TimeOnly ProductivityStartTime { get; set; }
        public TimeOnly ProductivityEndTime { get; set; }
        public UserSettingsViewModel.Language CurrentLanguage { get; set; }


        public UserSettingsModel() {

            // Default values
            NotificationSoundsEnabled = true;
            ProductivityStartTime = new TimeOnly(8, 00);
            ProductivityEndTime = new TimeOnly(20, 00);
            CurrentLanguage = UserSettingsViewModel.Language.EN;
        }
    }
}
