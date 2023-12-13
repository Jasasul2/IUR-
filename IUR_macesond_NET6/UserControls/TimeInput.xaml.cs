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
    /// Interakční logika pro TimeInput.xaml
    /// </summary>
    public partial class TimeInput : UserControl
    {
        public TimeInput()
        {
            InitializeComponent();
        }

        // Dependency properties for binding
        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("FirstValue", typeof(string), typeof(TimeInput));

        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("SecondValue", typeof(string), typeof(TimeInput));

        public string Hours
        {
            get { return (string)GetValue(HoursProperty); }
            set { SetValue(HoursProperty, value); }
        }

        public string Minutes
        {
            get { return (string)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
    }
}
