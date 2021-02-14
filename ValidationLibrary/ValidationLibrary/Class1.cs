using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Validation
{
    public class Validation1 : ValidationRule, Validation.Interface.IValidation
    {
        public string Name => "Minimal length";

        public string Description => "At least 5 characters";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string val = value as string;
            if (val == null || val.Length < 5) return new ValidationResult(false, Description);
            else return ValidationResult.ValidResult;
        }
    }
}
