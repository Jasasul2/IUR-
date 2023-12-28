using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using IUR_macesond_NET6.ViewModels;
using Newtonsoft.Json.Linq;

namespace IUR_macesond_NET6.Models
{
    class MainModel
    {
        public DateOnly FirstDate { get; set; }
        public DateOnly SelectedDate { get; set; }
        
        public int TotalPoints { get; set; }


        public MainModel() {

            // Default values
            FirstDate = DateOnly.FromDateTime(DateTime.Now);
            SelectedDate = DateOnly.FromDateTime(DateTime.Now);

            TotalPoints = 0;
        }

        public void SetAttributes(MainViewModel mainViewModel)
        {
            FirstDate = DateOnly.FromDateTime(mainViewModel.FirstDate);
            SelectedDate = DateOnly.FromDateTime(mainViewModel.SelectedDateTime);
            
            TotalPoints = mainViewModel.TotalPoints;
        }
    }
}
