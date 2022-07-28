using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
  public class ReactionInfoMapper : IReactionInfoMapper
  {
    public ReactionInfo Map(DbImage dbImage)
    {
      return dbImage is null
        ? null
        : new ReactionInfo
        {
          Id = dbImage.Id,
          Name = dbImage.Name,
          Content = dbImage.Content,
          Extension = dbImage.Extension
        };
    }
  }
}
