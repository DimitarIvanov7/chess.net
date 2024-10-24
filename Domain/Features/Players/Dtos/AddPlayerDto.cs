using System.ComponentModel.DataAnnotations;
using WebApplication3.WebApi.Validation;

namespace WebApplication3.Model.DTO.PlayerDto
{
    public class AddPlayerDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "User name must be at least 5 characters long")]
        [MaxLength(10, ErrorMessage = "User name must be less than 11 charactres long")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [PasswordComplexity(ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string[] Roles { get; set; }
    }
}
