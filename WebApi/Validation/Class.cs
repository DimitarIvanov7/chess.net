

using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebApplication3.WebApi.Validation
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required");
            }

            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);
            var hasSpecialChar = Regex.IsMatch(password, @"[\W_]");

            if (!hasUpperCase || !hasLowerCase || !hasDigit || !hasSpecialChar)
            {
                return new ValidationResult("Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character");
            }

            return ValidationResult.Success;
        }
    }
}
