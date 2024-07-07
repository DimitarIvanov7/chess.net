using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Model.Domain
{
    public class Image
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
