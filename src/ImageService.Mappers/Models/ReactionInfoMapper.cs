using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Models;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
  public class ReactionInfoMapper : IReactionInfoMapper
  {
    public List<ReactionInfo> Map(List<DbReaction> dbReactions)
    {
      return dbReactions is null
        ? null
        : dbReactions.Select(dbReaction => new ReactionInfo
        {
          Id = dbReaction.Id,
          Name = dbReaction.Name,
          Unicode = dbReaction.Unicode,
          Content = dbReaction.Content,
          Extension = dbReaction.Extension,
          GroupId = dbReaction.GroupId,
          IsActive = dbReaction.IsActive
        }).ToList();
    }
  }
}
