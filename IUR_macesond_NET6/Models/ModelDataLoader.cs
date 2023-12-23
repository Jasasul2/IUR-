using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IUR_macesond_NET6.ViewModels;
using System.IO;

namespace IUR_macesond_NET6.Models
{
    internal class ModelDataLoader
    {
        #region Settings
        private const string _saveDataUserSettingsFileName = "UserSettings.json";

        #endregion

        private const string _saveDataTaskDictionary = "TaskDictionary.json";
        private const string _saveDataUserLevel = "UserLevel.json";
        private const string _saveDataUserXP = "UserXP.json";
        private const string _saveDataTaskLibrary = "TaskLibrary.json";

        private string appDataFolder;

        public ModelDataLoader() {

            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            appDataFolder = Path.Combine(localAppData, "BeProductive!");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
        }


        public UserSettingsModel LoadUserSettings()
        {
            string filePath = Path.Combine(appDataFolder, _saveDataUserSettingsFileName);

            // Nothing saved
            if (!File.Exists(filePath))
            {
                return new UserSettingsModel();
            }

            UserSettingsModel loadedSettings = JsonConvert.DeserializeObject<UserSettingsModel>(File.ReadAllText(filePath));
            // Loading failed 
            if (loadedSettings == null)
            {
                return new UserSettingsModel();
            }

            return loadedSettings;
        }

        public void SaveUserSettings(UserSettingsModel userSettingsModel)
        {
            string filePath = Path.Combine(appDataFolder, _saveDataUserSettingsFileName);

            string json = JsonConvert.SerializeObject(userSettingsModel);
            if(json == null)
            {
                return;
            }

            File.WriteAllText(filePath, json);
        }
    }
}
