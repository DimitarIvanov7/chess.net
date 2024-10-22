using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Domain.Features.Images.Dtos;
using WebApplication3.Domain.Features.Images.Entities;
using WebApplication3.Domain.Features.Images.Repository;


namespace WebApplication3.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        public IimageRepository ImageRepository;

        public ImagesController(IimageRepository imageRepository)
        {
            ImageRepository = imageRepository;
        }



        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult<ImageEntity>> Upload([FromForm] ImageUploadDto imageUploadDto)
        {


            ValidateFileUpload(imageUploadDto);

            if (ModelState.IsValid)
            {

                //convert dto to Domain model

                var imageDomainModel = new ImageEntity
                {
                    File = imageUploadDto.File,
                    FileExtension = Path.GetExtension(imageUploadDto.File.FileName),
                    FileSizeInBytes = imageUploadDto.File.Length,
                    FileName = imageUploadDto.File.FileName,
                };


                await ImageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);

            }

            return BadRequest(ModelState);

        }


        private void ValidateFileUpload(ImageUploadDto imageUploadDto)
        {
            string[] allowedExtensions = new string[] { ".jpg", ".png", "jpeg" };


            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extenstion");
            }


            if (imageUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size should be less than 10 MB");
            }
        }

    }
}
