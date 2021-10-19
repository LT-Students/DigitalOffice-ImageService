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
    Task<List<DbImageProject>> GetAsync(List<Guid> imagesIds);

    Task<bool> RemoveAsync(List<Guid> imagesIds);

    Task<List<Guid>> CreateAsync(List<DbImageProject> imagesProject);

    Task<DbImageProject> GetAsync(Guid imageId);
  }
}
