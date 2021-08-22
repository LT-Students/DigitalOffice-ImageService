using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImageNewsResponseMapper : IImageNewsResponseMapper
    {
        public ImageNewsResponse Map(DbImagesNews dbImagesNews)
        {
            return new ImageNewsResponse
            {
                Id = dbImagesNews.Id,
                Content = dbImagesNews.Content,
                Name = dbImagesNews.Name,
                Extension = dbImagesNews.Extension
            };
        }
    }
}
