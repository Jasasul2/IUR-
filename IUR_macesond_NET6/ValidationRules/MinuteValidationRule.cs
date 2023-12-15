﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IUR_macesond_NET6.ValidationRules
{
    public class MinuteValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;


            if (int.TryParse(input, out int integerValue))
            {
                // Conversion successful, check if the integer value is between 0 and 24
                if (integerValue >= 0 && integerValue < 59)
                {
                    return ValidationResult.ValidResult;
                }
            }

            return new ValidationResult(false, "Please enter a valid number from 0 to 60.");
        }
    }
}