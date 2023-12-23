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
            CurrentLanguage = UserSettingsViewModel.Language.EN;
            NotificationSoundsEnabled = true;
            ProductivityStartTime = new TimeOnly(9, 45);
            ProductivityEndTime = new TimeOnly(22, 30);
        }
    }
}
