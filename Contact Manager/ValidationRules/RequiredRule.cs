using System.Globalization;
using System.Windows.Controls;

namespace Contact_Manager.ValidationRules
{
    public class RequiredRule : ValidationRule
    {
        public string FieldName
        {
            get;
            set;
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                if (FieldName.Length == 0)
                    FieldName = "Field";
                return new ValidationResult(false, FieldName + " is mandatory.");
            }
            return ValidationResult.ValidResult;
        }
    }
}