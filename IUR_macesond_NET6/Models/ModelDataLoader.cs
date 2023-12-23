using IUR_macesond_NET6.Support;
using IUR_macesond_NET6.ViewModels;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace IUR_macesond_NET6.Models
{
    internal class ModelDataLoader
    {
        private string appDataFolder;

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

        public void SaveUserSettings(UserSettingsModel userSettingsModelToSave)
        {
            string filePath = Path.Combine(appDataFolder, _saveDataUserSettingsFileName);

            string json = JsonConvert.SerializeObject(userSettingsModelToSave);
            if (json == null)
            {
                return;
            }

            File.WriteAllText(filePath, json);
        }


        #endregion


        #region MainModel   

        private const string _saveDataMainModel = "MainModel.json";

        public MainModel LoadMainModel()
        {
            string filePath = Path.Combine(appDataFolder, _saveDataMainModel);

            // Nothing saved
            if (!File.Exists(filePath))
            {
                return new MainModel();
            }

            MainModel loadedMainModel = JsonConvert.DeserializeObject<MainModel>(File.ReadAllText(filePath));
            // Loading failed 
            if (loadedMainModel == null)
            {
                return new MainModel();
            }

            return loadedMainModel;
        }

        public void SaveMainModel(MainModel mainModelToSave)
        {
            string filePath = Path.Combine(appDataFolder, _saveDataMainModel);

            string json = JsonConvert.SerializeObject(mainModelToSave);
            if (json == null)
            {
                return;
            }

            File.WriteAllText(filePath, json);
        }


        #endregion

        private const string _saveDataTaskLibrary = "TaskLibrary.json";

        #region TaskDictionary
        private const string _saveDataTaskDictionary = "TaskDictionary.json";

        public Dictionary<DateOnly, ObservableCollection<TaskModel>> LoadTaskDictionary()
        {
            string filePath = Path.Combine(appDataFolder, _saveDataTaskDictionary);

            // Nothing saved
            if (!File.Exists(filePath))
            {
                return new Dictionary<DateOnly, ObservableCollection<TaskModel>>();
            }

            Dictionary<DateOnly, ObservableCollection<TaskModel>> loadedDictionary = JsonConvert.DeserializeObject<Dictionary<DateOnly, ObservableCollection<TaskModel>>>(File.ReadAllText(filePath));
            // Loading failed 
            if (loadedDictionary == null)
            {
                return new Dictionary<DateOnly, ObservableCollection<TaskModel>>();
            }

            return loadedDictionary;
        }

        public void SaveTaskDictionary(Dictionary<DateOnly, ObservableCollection<TaskModel>> dictionaryToSave)
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

        private bool _saveDeleted = false;
        public bool SaveDeleted
        {
            get => _saveDeleted;
        }

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

                _saveDeleted = true;
                _mainViewModelReference.ExitApplication();
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
