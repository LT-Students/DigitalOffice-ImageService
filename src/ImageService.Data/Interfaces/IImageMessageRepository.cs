using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
  [AutoInject]
  public interface IImageMessageRepository
  {
    Task<List<Guid>> CreateAsync(List<DbImageMessage> imagesMessages);

    Task<List<DbImageMessage>> GetAsync(List<Guid> imagesIds);

    Task<DbImageMessage> GetAsync(Guid imageId);

    Task<bool> RemoveAsync(List<Guid> imagesIds);
  }
}
