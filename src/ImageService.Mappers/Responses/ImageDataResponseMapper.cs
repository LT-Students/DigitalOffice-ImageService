using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.User;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImagesUser dbImagesUser)
        {
            return new ImageDataResponse
            {
                Id = dbImagesUser.Id,
                Content = dbImagesUser.Content,
                Name = dbImagesUser.Name,
                Extension = dbImagesUser.Extension
            };
        }
    }
}
