using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Data.Interfaces;

[AutoInject]
public interface IImageRepository
{
  Task CreateAsync(ImageSource sourse, List<DbImage> dbImages);

  Task<List<DbImage>> GetAsync(ImageSource sourse, List<Guid> imagesIds);

  Task<DbImage> GetAsync(ImageSource sourse, Guid imageId);

  Task RemoveAsync(ImageSource sourse, List<Guid> imagesIds);
}
