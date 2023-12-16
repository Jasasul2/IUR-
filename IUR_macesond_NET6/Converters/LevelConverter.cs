using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace IUR_macesond_NET6.Converters
{
    public class LevelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] != null && values[1] != null)
            {
                int levelNum = (int)values[0];
                string levelLabel = values[1].ToString();

                return $"{levelLabel} {levelNum}";
            }

            return "Level 0";

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
