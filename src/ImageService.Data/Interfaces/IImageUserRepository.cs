using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
  [AutoInject]
  public interface IImageUserRepository
  {
    Task<List<Guid>> CreateAsync(List<DbImageUser> imagesUsers);

    Task<List<DbImageUser>> GetAsync(List<Guid> imagesIds);

    Task<DbImageUser> GetAsync(Guid imageId);

    Task<bool> RemoveAsync(List<Guid> imagesIds);
  }
}
