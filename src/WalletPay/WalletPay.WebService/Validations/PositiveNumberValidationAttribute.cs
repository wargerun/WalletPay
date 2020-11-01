using System.ComponentModel.DataAnnotations;

namespace WalletPay.WebService.Validations
{
    public class PositiveNumberValidationAttribute : ValidationAttribute
    {
        public PositiveNumberValidationAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (decimal)value < 0 
                ? new ValidationResult(ErrorMessage ?? $"The value must be positive.")
                : ValidationResult.Success;
        }
    }
}
