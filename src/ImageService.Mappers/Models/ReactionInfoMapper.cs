using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
  public class ReactionInfoMapper : IReactionInfoMapper
  {
    public List<ReactionInfo> Map(List<DbImage> dbReactions)
    {
      return dbReactions is null
        ? null
        : dbReactions.Select(dbImage => new ReactionInfo
        {
          Id = dbImage.Id,
          Name = dbImage.Name,
          Content = dbImage.Content,
          Extension = dbImage.Extension
        }).ToList();
    }
  }
}
