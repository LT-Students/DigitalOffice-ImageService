using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.User;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces
{
    [AutoInject]
    public interface IImageUserResponseMapper
    {
        ImageUserResponse Map(DbImagesUser dbImagesUser);
    }
}
