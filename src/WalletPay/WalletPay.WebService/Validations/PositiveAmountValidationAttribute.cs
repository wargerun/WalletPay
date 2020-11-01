using System.ComponentModel.DataAnnotations;

namespace WalletPay.WebService.Validations
{
    public class PositiveAmountValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (decimal)value < 0 
                ? new ValidationResult(ErrorMessage ?? $"The value must be positive.")
                : ValidationResult.Success;
        }
    }
}
