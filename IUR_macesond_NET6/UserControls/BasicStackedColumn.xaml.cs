using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IUR_macesond_NET6.UserControls
{
    /// <summary>
    /// Interakční logika pro BasicStackedColumn.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for StackedColumnExample.xaml
    /// </summary>
    public partial class BasicStackedColumn : UserControl
    {
        public BasicStackedColumn()
        {
            InitializeComponent();

            //SeriesCollection = new SeriesCollection
            //{
            //    new StackedColumnSeries
            //    {
            //        Values = new ChartValues<double> {4, 5, 6, 8},
            //        StackMode = StackMode.Values, 
            //        Fill = Brushes.Orange,
            //    },
            //    new StackedColumnSeries
            //    {
            //        Values = new ChartValues<double> {2, 5, 6, 7},
            //        StackMode = StackMode.Values,
            //    },
            //    new StackedColumnSeries
            //    {
            //        Values = new ChartValues<double> {2, 5, 6, 7},
            //        StackMode = StackMode.Values,
            //    }
            //};

            //Labels = new[] { "Chrome", "Mozilla", "Opera", "IE" };

            DataContext = this;
        }


        // Dependency properties for binding
        public static readonly DependencyProperty SeriesCollectionProperty =
            DependencyProperty.Register("SeriesCollection", typeof(SeriesCollection), typeof(BasicStackedColumn));

        public SeriesCollection SeriesCollection
        {
            get { return (SeriesCollection)GetValue(SeriesCollectionProperty); }
            set { SetValue(SeriesCollectionProperty, value); }
        }

        public static readonly DependencyProperty LabelsProperty =
            DependencyProperty.Register("Labels", typeof(string[]), typeof(BasicStackedColumn));

        public string[] Labels
        {
            get { return (string[])GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        public static readonly DependencyProperty XLabelProperty =
            DependencyProperty.Register("XLabel", typeof(string), typeof(BasicStackedColumn));

        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        public static readonly DependencyProperty YLabelProperty =
            DependencyProperty.Register("YLabel", typeof(string), typeof(BasicStackedColumn));

        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set { SetValue(LabelsProperty, value); }
        }

    }
}

