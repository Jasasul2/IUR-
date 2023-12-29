using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUR_macesond_NET6.Support;
using LiveCharts;
using LiveCharts.Wpf;
using Windows.Globalization;

namespace IUR_macesond_NET6.ViewModels
{
    class ProductivityGraphViewModel : ViewModelBase
    {
        #region Constructor 

        private MainViewModel _mainViewModelReference;
        public ProductivityGraphViewModel(MainViewModel mainViewModelReference) { 
        
            _mainViewModelReference = mainViewModelReference;
            UpdateProductivityGraph();
        }

        #endregion

        #region GraphProperties

        private SeriesCollection _seriesCollection;

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                SetProperty(ref _seriesCollection, value);
            }
        }

        private string[] _labels;

        public string[] Labels
        {
            get { return _labels; }
            set 
            {
                SetProperty(ref _labels, value);
            }
        }

        public void UpdateProductivityDefault()
        {
            SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {4, 5, 6, 8},
                    StackMode = StackMode.Values,
                },
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {2, 5, 6, 7},
                    StackMode = StackMode.Values,
                },
                new StackedColumnSeries
                {
                    Values = new ChartValues<double> {2, 5, 6, 7},
                    StackMode = StackMode.Values,
                }
            };

            Labels = new[] { "Chrome", "Mozilla", "Opera", "IE" };
        }

        public void UpdateProductivityGraph()
        {
            // Part 1 - Count day span

            // Subtract two DateOnly variables
            int daySpan = Math.Clamp(
                DateOnly.FromDateTime(DateTime.Now).DayNumber 
                - DateOnly.FromDateTime(_mainViewModelReference.FirstDate).DayNumber + 1,
                1, 7);

            // Part 2 - for each difficulty iterate trough each day in the day span

            Difficulty[] diffArray = (Difficulty[])Enum.GetValues(typeof(Difficulty));

            SeriesCollection = new SeriesCollection();
            Labels = new string[daySpan];

            foreach (Difficulty difficulty in diffArray)
            {
                // Part 3 - for each day, count how many tasks of this difficulty there are 

                DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
                StackedColumnSeries difficultyStack = new StackedColumnSeries();
                difficultyStack.Title = difficulty.ToString();
                difficultyStack.StackMode = StackMode.Values;
                difficultyStack.Values = new ChartValues<int>();

                for (int i = daySpan - 1; i >= 0; i--)
                {
                    DateOnly date = startDate.AddDays(-i);
                    string formattedDate = date.ToString("dd/MM/yyyy");
                    if(!Labels.Contains(formattedDate))
                    {
                        Labels[daySpan - 1 - i] = formattedDate;
                    }

                    int diffMatchCount = 0;
                    foreach (TaskViewModel taskVM in _mainViewModelReference.DateToTaskListDictionary[date]) 
                    {
                        if(taskVM.TaskDifficulty == difficulty && taskVM.MarkedForCompletion)
                        {
                            diffMatchCount++;   
                        }
                    }
                    difficultyStack.Values.Add(diffMatchCount);
                }
                SeriesCollection.Add(difficultyStack);
            }


        }
    }

    #endregion
}
