using IUR_macesond_NET6.ViewModels;
using Microsoft.VisualBasic;
using System;
using System.CodeDom;
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

                    case "Preparation Part of the Day":
                        return "Přípravná část dne";

                    case "Productive Part of the Day":
                        return "Produktivní část dne";

                    case "Resting Part of the Day":
                        return "Odpočinková část dne";

                    case "Easy":
                        return "Jednoduché";

                    case "Medium":
                        return "Středně obtížné";

                    case "Hard":
                        return "Těžké";
                }
            }

            return stringToTranslate;
        }

        public Translator() { 
        
        }
    }
}
