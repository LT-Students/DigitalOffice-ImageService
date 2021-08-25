using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImagesNews dbImagesNews)
        {
            if (dbImagesNews == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImagesNews.Id,
                Content = dbImagesNews.Content,
                Name = dbImagesNews.Name,
                Extension = dbImagesNews.Extension
            };
        }

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
        public ImageDataResponse Map(DbImagesUser dbImagesUser)
        {
            if (dbImagesUser == null)
            {
                return null;
            }

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
