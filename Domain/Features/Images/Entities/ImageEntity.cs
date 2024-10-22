using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Domain.Features.Images.Entities
{
    public class ImageEntity
    {

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; } 



    }
}
