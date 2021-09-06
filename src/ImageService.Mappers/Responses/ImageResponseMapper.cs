using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
  public class ImageResponseMapper : IImageResponseMapper
  {
    public ImageResponse Map(DbImageNews dbImagesNews)
    {
      if (dbImagesNews == null)
      {
        return null;
      }

      return new ImageResponse
      {
        Id = dbImagesNews.Id,
        Content = dbImagesNews.Content,
        Name = dbImagesNews.Name,
        Extension = dbImagesNews.Extension
      };
    }

    public ImageResponse Map(DbImageMessage dbImageMessage)
    {
      if (dbImageMessage == null)
      {
        return null;
      }

      return new ImageResponse
      {
        Id = dbImageMessage.Id,
        Content = dbImageMessage.Content,
        Name = dbImageMessage.Name,
        Extension = dbImageMessage.Extension
      };
    }

    public ImageResponse Map(DbImageProject dbImageProject)
    {
      if (dbImageProject == null)
      {
        return null;
      }

      return new ImageResponse
      {
        Id = dbImageProject.Id,
        Content = dbImageProject.Content,
        Name = dbImageProject.Name,
        Extension = dbImageProject.Extension
      };
    }

    public ImageResponse Map(DbImageUser dbImagesUser)
    {
      if (dbImagesUser == null)
      {
        return null;
      }

      return new ImageResponse
      {
        Id = dbImagesUser.Id,
        Content = dbImagesUser.Content,
        Name = dbImagesUser.Name,
        Extension = dbImagesUser.Extension
      };
    }
  }
}
