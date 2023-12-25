using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace IUR_macesond_NET6.ViewModels
{
    class Timer
    {
        private MainViewModel _mainViewModelReference;
        private DispatcherTimer _dispatcherTimer;    

        public Timer(MainViewModel mainViewModel) 
        {
            _mainViewModelReference = mainViewModel;

            //  DispatcherTimer setup
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            _mainViewModelReference.CurrentDateTime = DateTime.Now;

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
