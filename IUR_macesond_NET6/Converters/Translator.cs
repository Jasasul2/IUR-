using IUR_macesond_NET6.ViewModels;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IUR_macesond_NET6.ViewModels.UserSettingsViewModel;

namespace IUR_macesond_NET6.Converters
{
    // This class is used for static translations during validation and/or converters
    class Translator
    {
        private static Language _language;

        public static Language CurrentLanguage
        {
            get => _language;
            set => _language = value;
        }

        public static ObservableCollection<string> translateCollection(ObservableCollection<string> input)
        {
            ObservableCollection<string> translatedStrings = new ObservableCollection<string>();
            foreach (string s in input)
            {
                string translatedS = TranslateToCzech(s);
                translatedStrings.Add(translatedS);
            }
            return translatedStrings;
        }

        public static string TranslateToCzech(string stringToTranslate)
        {
            if (CurrentLanguage == Language.CZ)
            {
                switch (stringToTranslate)
                {
                    case "Please enter a valid number from 0 to 59.":
                        return "Prosím zadejte platné číslo od 0 do 59.";

                    case "Please enter a valid number from 0 to 23.":
                        return "Prosím zadejte platné číslo od 0 do 23.";
                }
            }

            return stringToTranslate;
        }

        public static string TranslateToEnglish(string stringToTranslate)
        {
            if (CurrentLanguage == Language.CZ)
            {
                switch (stringToTranslate)
                {
                    case "Obojí":
                        return "Both";

                    case "Žádné":
                        return "None";
                }
            }

            return stringToTranslate;
        }

        public Translator() { 
        
        }
    }
}
