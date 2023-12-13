using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IUR_macesond.ValidationRules
{
    public class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] numbers = input.Split(' ');

                if (numbers.Length != 2 || !double.TryParse(numbers[0], out _) || !double.TryParse(numbers[1], out _))
                    return new ValidationResult(false, "Please enter two valid numbers separated by space.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
