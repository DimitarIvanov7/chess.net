using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model.DTO.AuthDto
{
    public class LoginRequestDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }





    }
}
