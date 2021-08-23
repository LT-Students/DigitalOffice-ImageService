using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImageResponseMapper : IImageResponseMapper
    {
        public ImageDataResponse  Map(DbImagesProject dbImagesProject)
        {
            return new ImageDataResponse
            {
                Id = dbImagesProject.Id,
                Content = dbImagesProject.Content,
                Name = dbImagesProject.Name,
                Extension = dbImagesProject.Extension
            };
        }
    }
}
