using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
  public class DbImageProjectMapper : IDbImageProjectMapper
  {
    public DbImageProject Map(CreateImageData createImageData, Guid? parentId = null, string content = null, string extension = null)
    {
      if (createImageData == null)
      {
        return null;
      }

      return new DbImageProject
      {
        Id = Guid.NewGuid(),
        ParentId = parentId,
        Name = createImageData.Name,
        Content = content ?? createImageData.Content,
        Extension = extension ?? createImageData.Extension,
        CreatedAtUtc = DateTime.UtcNow,
        CreatedBy = createImageData.CreatedBy
      };
    }
  }
}
