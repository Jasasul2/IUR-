using IUR_macesond_NET6.Support;
using IUR_macesond_NET6.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace IUR_macesond_NET6.Models
{
    internal class ModelDataLoader
    {
        #region UserSettings
        private const string _saveDataUserSettingsFileName = "UserSettings.json";

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
            if (json == null)
            {
                return;
            }

            File.WriteAllText(filePath, json);
        }


        #endregion

        private const string _saveDataUserLevel = "UserLevel.json";
        private const string _saveDataUserXP = "UserXP.json";
        private const string _saveDataTaskLibrary = "TaskLibrary.json";

        private string appDataFolder;

        #region TaskDictionary
        private const string _saveDataTaskDictionary = "TaskDictionary.json";

        public Dictionary<DateOnly, ObservableCollection<TaskViewModel>> LoadTaskDictionary()
        {
            string filePath = Path.Combine(appDataFolder, _saveDataTaskDictionary);

            // Nothing saved
            if (!File.Exists(filePath))
            {
                return new Dictionary<DateOnly, ObservableCollection<TaskViewModel>>();
            }

            Dictionary<DateOnly, ObservableCollection<TaskViewModel>> loadedDictionary = JsonConvert.DeserializeObject<Dictionary<DateOnly, ObservableCollection<TaskViewModel>>>(File.ReadAllText(filePath));
            // Loading failed 
            if (loadedDictionary == null)
            {
                return new Dictionary<DateOnly, ObservableCollection<TaskViewModel>>();
            }

            return loadedDictionary;
        }

        public void SaveTaskDictionary(Dictionary<DateOnly, ObservableCollection<TaskViewModel>> dictionaryToSave)
        {
            string filePath = Path.Combine(appDataFolder, _saveDataTaskDictionary);

            string json = JsonConvert.SerializeObject(dictionaryToSave);
            if (json == null)
            {
                return;
            }

            File.WriteAllText(filePath, json);
        }

        #endregion


        #region Constructor

        private MainViewModel _mainViewModelReference;

        public ModelDataLoader(MainViewModel mainViewModelReference)
        {

            _mainViewModelReference = mainViewModelReference;

            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            appDataFolder = Path.Combine(localAppData, "BeProductive!");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
        }

        #endregion


        #region DeleteAllSaves

        public void DeleteAllSaves(object obj)
        {
            // Check if the directory exists
            if (Directory.Exists(appDataFolder))
            {
                // Get all files within the directory
                string[] files = Directory.GetFiles(appDataFolder);

                // Delete each file in the directory
                foreach (string file in files)
                {
                    File.Delete(file);
                }

                _mainViewModelReference.Reload();
            }
        }

        private RelayCommand _deleteAllSavesCommand;

        public RelayCommand DeleteAllSavesCommand
        {
            get { return _deleteAllSavesCommand ?? (_deleteAllSavesCommand = new RelayCommand(DeleteAllSaves, DeleteAllSavesCommandCanExecute)); }
        }

        private bool DeleteAllSavesCommandCanExecute(object obj)
        {
            return true;
        }


        #endregion
    }
}
