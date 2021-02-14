using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Validation
{
    public class Validation3 : ValidationRule,Interface.IValidation
    {
        public string Name => "Phone number";

        public string Description => "String needs to have xxx-xxx-xxx format";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string val = value as string;
            if (val == null || val.Length != 11) return new ValidationResult(false, Description);
            string[] tab = val.Split('-');
            if (tab.Length != 3) return new ValidationResult(false, Description);

            foreach (var part in tab)
            {
                if (part.Length != 3) return new ValidationResult(false, Description);
                foreach (var c in part)
                {
                    if (!char.IsDigit(c)) return new ValidationResult(false, Description);
                }
            }

            return ValidationResult.ValidResult;
        }
    }
}
