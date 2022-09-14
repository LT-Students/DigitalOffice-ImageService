using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;

namespace LT.DigitalOffice.ImageService.Mappers.Responses;

public class GetReactionResponseMapper : IGetReactionResponseMapper
{
  public GetReactionResponse Map(DbReaction dbReaction)
  {
    return dbReaction is null
      ? null
      : new GetReactionResponse
      {
        Id = dbReaction.Id,
        Name = dbReaction.Name,
        Unicode = dbReaction.Unicode,
        Content = dbReaction.Content,
        Extension = dbReaction.Extension,
        GroupId = dbReaction.GroupId,
        IsActive = dbReaction.IsActive
      };
  }
}
