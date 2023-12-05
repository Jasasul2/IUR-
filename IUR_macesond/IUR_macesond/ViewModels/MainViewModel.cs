using IUR_macesond.Support;
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
    internal class MainViewModel : ViewModelBase
    {
        // This Main View Model consists of both the user settings and the Task lists for every saved day

        public UserSettingsViewModel UserSettings { get; set; } = new UserSettingsViewModel();


    //    //public ObservableCollection<WeatherCardViewModel> WeatherCards { get; set; } = new ObservableCollection<WeatherCardViewModel>();
    //    private RelayCommand _AddTaskCommand;
    //    public RelayCommand AddTaskCommand
    //    {
    //        get { return _AddTaskCommand ?? (_AddTaskCommand = new RelayCommand(AddTask, AddTaskCanExecute)); }
    //    }

    //    private void AddTask(object obj)
    //    {
    //        //WeatherCards.Add(new WeatherCardViewModel(this, CityToBeAdded));
    //        //CityToBeAdded = "";
    //    }

    //    private bool AddTaskCanExecute(object obj)
    //    {
    //        return true;
    //    }

    //    public MainViewModel()
    //    {
    //        //WeatherCards.Add(new WeatherCardViewModel(this, "Praha"));
    //        //WeatherCards.Add(new WeatherCardViewModel(this, "Brno"));
    //        //WeatherCards.Add(new WeatherCardViewModel(this, "Ostrava"));
    //        //WeatherCards.Add(new WeatherCardViewModel(this, "Jihlava"));
    //        //WeatherCards.Add(new WeatherCardViewModel(this, "Rakovník"));
    //    }
    }
}
