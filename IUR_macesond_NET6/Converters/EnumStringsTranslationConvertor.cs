using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Xml.Linq;

namespace IUR_macesond_NET6.Converters
{
    public class EnumStringsTranslationConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is ObservableCollection<string> strings)
            {
                // Translate the strings based on the language
                return TranslateStrings(strings);
            }

            return "ERROR";

        }

        // Your translation logic here
        private string[] TranslateStrings(ObservableCollection<string> strings)
        {
            string[] translatedStrings = new string[strings.Count]; 
            for (int i = 0; i < strings.Count; i++)
            {
                string s = strings[i];
                translatedStrings[i] = Translator.TranslateToCzech(s);
            }
            return translatedStrings; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
