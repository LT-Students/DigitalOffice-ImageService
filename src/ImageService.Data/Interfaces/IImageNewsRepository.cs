using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
  [AutoInject]
  public interface IImageNewsRepository
  {
    Task<List<Guid>> CreateAsync(List<DbImageNews> imagesNews);

    Task<List<DbImageNews>> GetAsync(List<Guid> imageIds);

    Task<DbImageNews> GetAsync(Guid imageId);

    Task<bool> RemoveAsync(List<Guid> imageIds);
  }
}
