using System;
using System.Collections.Generic;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
  [AutoInject]
  public interface IImageProjectRepository
  {
    List<DbImageProject> Get(List<Guid> imageIds);

    bool Remove(List<Guid> imageIds);

    List<Guid> Create(List<DbImageProject> imagesProject);

    DbImageProject Get(Guid imageId);
  }
}
