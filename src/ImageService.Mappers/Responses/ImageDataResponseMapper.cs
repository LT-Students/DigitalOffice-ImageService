using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImagesMessage dbImageMessage)
        {
            if (dbImageMessage == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImageMessage.Id,
                Content = dbImageMessage.Content,
                Name = dbImageMessage.Name,
                Extension = dbImageMessage.Extension
            };
        }
    }
}
