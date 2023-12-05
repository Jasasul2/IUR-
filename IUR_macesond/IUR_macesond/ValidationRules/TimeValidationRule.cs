
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IUR_macesond.ValidationRules
{
    internal class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (((string)value).Length < 2)
            {
                return new ValidationResult(false, "Location name must be at least two characters");
            }

            return ValidationResult.ValidResult;
        }
    }
}
