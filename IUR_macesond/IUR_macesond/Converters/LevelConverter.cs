using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IUR_macesond.Converters
{
    public class LevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                int levelNum = (int)value;
                return $"Level {levelNum}";
            }

            return "Level 0";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
