using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IImageDataMapper
  {
    ImageData Map(DbImageProject imageProject);
  }
}
