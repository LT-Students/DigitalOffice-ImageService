using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces
{
    [AutoInject]
    public interface IImageDataResponseMapper
    {
        ImageDataResponse Map(DbImagesMessage dbImageMessage);
        ImageDataResponse Map(DbImagesNews dbImagesNews);
        ImageDataResponse Map(DbImagesUser dbImagesUser);
    }
}
