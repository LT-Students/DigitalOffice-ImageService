using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses;

public class ImageResponseMapper : IImageResponseMapper
{
  public ImageResponse Map(DbImage dbImage)
  {
    return dbImage is null
      ? null
      : new ImageResponse
      {
        Id = dbImage.Id,
        Content = dbImage.Content,
        Name = dbImage.Name,
        Extension = dbImage.Extension
      };
  }
}
