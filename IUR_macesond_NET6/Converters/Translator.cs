using IUR_macesond_NET6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IUR_macesond_NET6.ViewModels.UserSettingsViewModel;

namespace IUR_macesond_NET6.Converters
{
    class Translator
    {
        private static Language _language;

        public static Language CurrentLanguage
        {
            get => _language;
            set => _language = value;
        }

        public static string TranslateToCzech(string stringToTranslate)
        {
            if (CurrentLanguage == Language.Czech)
            {
                switch (stringToTranslate)
                {
                    case "English":
                        return "Anglicky";

                    case "Czech":
                        return "Česky";

                    case "Sound":
                        return "Zvuk";

                    case "Text":
                        return "Text";

                    case "Both":
                        return "Obojí";

                    case "None":
                        return "Žádné";
                }
            }

            return stringToTranslate;
        }

        public static string TranslateToEnglish(string stringToTranslate)
        {
            if (CurrentLanguage == Language.Czech)
            {
                switch (stringToTranslate)
                {
                    case "Anglicky":
                        return "English";

                    case "Česky":
                        return "Czech";

                    case "Zvuk":
                        return "Sound";
                    
                    case "Text":
                        return "Text";

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
