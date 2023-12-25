using System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IUR_macesond_NET6.Converters
{
    public class TimeFixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (int.TryParse(stringValue, out int intValue))
                {
                    if (intValue >= 0 && intValue <= 9)
                    {
                        return $"0{intValue}";
                    }
                }
            }

            // Return the original string if it doesn't meet the conditions
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (stringValue.StartsWith("0") && stringValue.Length == 2)
                {
                    if (int.TryParse(stringValue.Substring(1), out int intValue))
                    {
                        if (intValue >= 0 && intValue <= 9)
                        {
                            return intValue.ToString();
                        }
                    }
                }
            }

            // Return the original string if it doesn't meet the conditions
            return value;
        }
    }
}
