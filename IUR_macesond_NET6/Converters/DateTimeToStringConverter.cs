using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace IUR_macesond_NET6.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        // Convert DateTime to string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("yyyy-dd-MM", CultureInfo.InvariantCulture);
            }

            return string.Empty; 
        }

        // Convert back from string to DateTime (if needed)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                DateTime resultDateTime;
                if (DateTime.TryParseExact(str, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultDateTime))
                {
                    return resultDateTime;
                }
            }

            return DependencyProperty.UnsetValue; 
        }
    }
}
