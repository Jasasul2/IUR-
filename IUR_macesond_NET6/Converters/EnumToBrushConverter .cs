using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

using IUR_macesond_NET6.ViewModels;

namespace IUR_macesond_NET6.Converters
{
    public class EnumToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 4 || !(values[0] is Enum) || !(values[1] is Brush) || !(values[2] is Brush) || !(values[3] is Brush))
                return DependencyProperty.UnsetValue;

            Difficulty difficulty = (Difficulty)values[0];
            Brush easyBrush = (Brush)values[1];
            Brush mediumBrush = (Brush)values[2];
            Brush hardBrush = (Brush)values[3];

            switch (difficulty)
            {
                case Difficulty.Medium:
                    return mediumBrush;
                case Difficulty.Hard:
                    return hardBrush;
                default:
                    return easyBrush;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
