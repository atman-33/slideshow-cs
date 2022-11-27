using System.Globalization;
using System.Windows.Controls;

namespace Slideshow.WPF.Services
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //return string.IsNullOrWhiteSpace((value ?? "").ToString())
            //    ? new ValidationResult(false, "Field is required.")
            //    : ValidationResult.ValidResult;

            return string.IsNullOrWhiteSpace((value ?? "").ToString()) 
                ? new ValidationResult(false, "入力必須項目です") : ValidationResult.ValidResult;

        }
    }
}
