using System;
using System.Collections.Generic;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
  [AutoInject]
  public interface IImageMessageRepository
  {
    List<Guid> Create(List<DbImageMessage> imagesMessages);

    List<DbImageMessage> Get(List<Guid> imageIds);

    DbImageMessage Get(Guid imageId);

    bool Remove(List<Guid> imageIds);
  }
}
