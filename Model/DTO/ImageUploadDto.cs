using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model.DTO
{
    public class ImageUploadDto
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public IFormFile File { get; set; }



    }
}
