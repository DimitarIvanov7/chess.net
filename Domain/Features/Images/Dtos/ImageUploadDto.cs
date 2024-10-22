using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Domain.Features.Images.Dtos
{
    public class ImageUploadDto
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public IFormFile File { get; set; }



    }
}
