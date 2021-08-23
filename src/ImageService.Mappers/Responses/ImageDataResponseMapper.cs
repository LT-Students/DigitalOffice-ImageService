using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImagesNews dbImagesNews)
        {
            if (dbImagesNews != null)
            {
                return new ImageDataResponse
                {
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
