using System.Globalization;
using System.Windows.Data;

namespace IUR_macesond_NET6.Converters
{
    public class PointsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] != null && values[1] != null)
            {
                string currentPoints = values[0].ToString();

                string localizedPointsText = values[1].ToString();

                return $"{currentPoints} {localizedPointsText}";
            }

            return string.Empty;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
