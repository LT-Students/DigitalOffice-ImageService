using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IImageResponseMapper
  {
    ImageResponse Map(DbImageProject dbImageProject);
    ImageResponse Map(DbImageMessage dbImageMessage);
    ImageResponse Map(DbImageNews dbImagesNews);
    ImageResponse Map(DbImageUser dbImagesUser);
  }
}
