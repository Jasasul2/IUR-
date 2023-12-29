using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUR_macesond_NET6.Support;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Windows.Globalization;
using IUR_macesond_NET6.Converters;
using System.Windows.Media;
using IUR_macesond_NET6.ViewModels;

namespace IUR_macesond_NET6.ViewModels
{
    class ProductivityGraphViewModel : ViewModelBase
    {
        #region Constructor 

        private MainViewModel _mainViewModelReference;
        public ProductivityGraphViewModel(MainViewModel mainViewModelReference) {

            _mainViewModelReference = mainViewModelReference;
            UpdateProductivityGraph(false);
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

        Dictionary<Difficulty, Color> _customColors = new Dictionary<Difficulty, Color>() 
            {
                {Difficulty.Easy, Color.FromRgb(177, 237, 138)},   
                {Difficulty.Medium, Color.FromRgb(237, 192, 138)},
                {Difficulty.Hard, Color.FromRgb(237, 138, 138)},
                // Add more custom colors as needed
            };


        private DateOnly previousCurrentDate = DateOnly.FromDateTime(DateTime.Now);

        public void UpdateProductivityGraph(bool fromDateChange)
        {

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            if (fromDateChange && currentDate == previousCurrentDate) return;

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
                difficultyStack.Title = Translator.TranslateToCzech(difficulty.ToString());
                difficultyStack.StackMode = StackMode.Values;
                difficultyStack.Values = new ChartValues<int>();
                difficultyStack.Fill = new SolidColorBrush(_customColors[difficulty]);

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

            previousCurrentDate = currentDate;


        }
    }

    #endregion
}
