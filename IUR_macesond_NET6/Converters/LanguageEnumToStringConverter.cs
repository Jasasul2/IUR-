using IUR_macesond_NET6.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IUR_macesond_NET6.Converters
{
    public sealed class LanguageEnumToStringConverter : IValueConverter
    {
        // Conversion from Enum to string (for display in the UI)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return Translator.TranslateToCzech(value.ToString());
            }

            return null;
        }

        // Conversion from string to Enum 
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return "Error";

            string str = Translator.TranslateToEnglish(value.ToString());
                
            _ = Enum.TryParse(str, out UserSettingsViewModel.Language enumValue);  
            return enumValue; 
        }
    }
}
