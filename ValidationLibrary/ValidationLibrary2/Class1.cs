using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Validation
{
    public class Validation2 : ValidationRule, Interface.IValidation
    {
        public string Name => "Email address";

        public string Description => "String has to be in email adress format";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"[\w]+[@][\w][\w]+[.][\w][\w]+");
            if (value==null || regex.Match(value as string).Success) return ValidationResult.ValidResult;
            else return new ValidationResult(false, Description);
        }
    }
}
