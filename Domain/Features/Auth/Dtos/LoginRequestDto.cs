using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Domain.Features.Auth.Dtos
{
    public class LoginRequestDto
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }





    }
}
