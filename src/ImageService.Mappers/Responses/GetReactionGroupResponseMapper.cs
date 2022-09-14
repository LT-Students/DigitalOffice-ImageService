using System.Linq;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses;

public class GetReactionGroupResponseMapper : IGetReactionGroupResponseMapper
{
  public GetReactionGroupResponse Map(DbReactionGroup dbReactionGroup)
  {
    return dbReactionGroup is null
      ? null
      : new GetReactionGroupResponse
      {
        Name = dbReactionGroup.Name,
        IsActive = dbReactionGroup.IsActive,
        ReactionsInfo = dbReactionGroup.Reactions?.Select(r => new ReactionInfo
        {
          Id = r.Id,
          Name = r.Name,
          Unicode = r.Unicode,
          Content = r.Content,
          Extension = r.Extension,
          GroupId = r.GroupId,
          IsActive = r.IsActive
        }).ToList()
      };
  }
}
