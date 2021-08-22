using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.User;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageUserResponseMapper : IImageUserResponseMapper
    {
        public ImageUserResponse Map(DbImagesUser dbImagesUser)
        {
            return new ImageUserResponse
            {
                Id = dbImagesUser.Id,
                Content = dbImagesUser.Content,
                Name = dbImagesUser.Name,
                Extension = dbImagesUser.Extension
            };
        }
    }
}
