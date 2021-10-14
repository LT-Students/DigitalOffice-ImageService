using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
  [AutoInject]
  public interface IImageProjectRepository
  {
    Task<List<DbImageProject>> GetAsync(List<Guid> imageIds);

    Task<bool> RemoveAsync(List<Guid> imageIds);

    Task<List<Guid>> CreateAsync(List<DbImageProject> imagesProject);

    Task<DbImageProject> GetAsync(Guid imageId);
  }
}
