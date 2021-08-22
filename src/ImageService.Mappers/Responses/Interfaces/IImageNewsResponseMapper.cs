using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Models.Interfaces
{
    [AutoInject]
    public interface IImageNewsResponseMapper
    {
        ImageNewsResponse Map(DbImagesNews dbImagesNews);
    }
}
