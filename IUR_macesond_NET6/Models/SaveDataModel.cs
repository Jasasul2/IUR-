using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace IUR_macesond_NET6.Models
{
    internal class SaveDataModel
    {
        private const string _saveDataUserSettingsFileName = "UserSettings.json";
        private const string _saveDataTaskDictionary = "TaskDictionary.json";
        private const string _saveDataUserLevel = "UserLevel.json";
        private const string _saveDataUserXP = "UserXP.json";
        private const string _saveDataTaskLibrary = "TaskLibrary.json";

        private string localAppData;
        private string appDataFolder;


        public SaveDataModel() {

            localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            appDataFolder = Path.Combine(localAppData, "BeProductive!");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
        }
    }
}
