using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImagesResponseMapper : IImagesResponseMapper
    {
        public ImagesDataResponse  Map(DbImagesProject dbImagesProject)
        {
            return new ImagesDataResponse
            {
                Id = dbImagesProject.Id,
                Content = dbImagesProject.Content,
                Name = dbImagesProject.Name,
                Extension = dbImagesProject.Extension
            };
        }
    }
}
