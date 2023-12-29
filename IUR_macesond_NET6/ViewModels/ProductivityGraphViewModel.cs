using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IUR_macesond_NET6.Support;
using LiveCharts;
using LiveCharts.Wpf;

namespace IUR_macesond_NET6.ViewModels
{
    class ProductivityGraphViewModel : ViewModelBase
    {
        #region Constructor 

        private MainViewModel _mainViewModelReference;
        public ProductivityGraphViewModel(MainViewModel mainViewModelReference) { 
        
            _mainViewModelReference = mainViewModelReference;
            UpdateProductivityDefaultS();
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

        private String[] _labels;

        public String[] Labels
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
    }

    #endregion
}
