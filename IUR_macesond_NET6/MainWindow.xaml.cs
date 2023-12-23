using IUR_macesond_NET6.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IUR_macesond_NET6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModelReference;

        public MainWindow()
        {
            InitializeComponent();

            // Assuming you set DataContext in XAML or somewhere else
            _mainViewModelReference = (MainViewModel)this.DataContext;

            // Subscribe to the Closing event
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Call the method in the view model
            _mainViewModelReference.OnWindowClosing();
        }
    }
}