using System;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbImageNewsMapper
  {
    DbImageNews Map(CreateImageRequest request, Guid? parentId = null, string content = null);
  }
}
