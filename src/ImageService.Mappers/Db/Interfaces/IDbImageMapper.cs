using System;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
  [AutoInject]
  public interface IDbImageMapper
  {
    DbImage Map(
      CreateImageData createImageData,
      Guid? parentId = null,
      string content = null,
      string extension = null);

    DbImage Map(
      CreateImageRequest request,
      Guid? parentId = null,
      string content = null,
      string extension = null);
  }
}
