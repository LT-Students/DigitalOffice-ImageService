using System;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbImageMessageMapper
  {
    DbImageMessage Map(CreateImageData createImageData, Guid? parentId = null, string content = null);
  }
}
