using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImagesMessage dbImageMessage)
        public ImageDataResponse Map(DbImagesNews dbImagesNews)
        {
            if (dbImageMessage == null)
            if (dbImagesNews != null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImageMessage.Id,
                Content = dbImageMessage.Content,
                Name = dbImageMessage.Name,
                Extension = dbImageMessage.Extension
                    Id = dbImagesNews.Id,
                    Content = dbImagesNews.Content,
                    Name = dbImagesNews.Name,
                    Extension = dbImagesNews.Extension
            };
        }
            else
            {
                return null;
            }
        }
    }
}
