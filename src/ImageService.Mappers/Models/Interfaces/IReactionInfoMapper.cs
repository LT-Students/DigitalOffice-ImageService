using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IReactionInfoMapper
  {
    ReactionInfo Map(DbImage dbImage);
  }
}
