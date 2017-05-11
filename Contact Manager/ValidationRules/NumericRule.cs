using System.Globalization;
using System.Windows.Controls;

namespace Contact_Manager.ValidationRules
{
    public class NumericRule : ValidationRule
    {
        public string FieldName
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double result = double.NaN;
            if (double.TryParse((string) value, out result))
                return ValidationResult.ValidResult;
            else
                return new ValidationResult(false, FieldName + " must be a number");
        }
    }
}