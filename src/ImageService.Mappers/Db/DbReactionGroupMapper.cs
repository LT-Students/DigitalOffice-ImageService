using System;
using System.Linq;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db;

public class DbReactionGroupMapper : IDbReactionGroupMapper
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DbReactionGroupMapper(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }
  public DbReactionGroup Map(CreateReactionGroupRequest request)
  {
    if (request is null)
    {
      return null;
    }

    Guid groupId = Guid.NewGuid();

    return new DbReactionGroup
    {
      Id = groupId,
      Name = request.Name,
      IsActive = true,
      CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
      CreatedAtUtc = DateTime.UtcNow,
      Reactions = request.ReactionList.Select(r => new DbReaction
      {
        Id = Guid.NewGuid(),
        Name = r.Name,
        Unicode = r.Unicode,
        Content = r.Content,
        Extension = r.Extension,
        GroupId = groupId,
        IsActive = true,
        CreatedAtUtc = DateTime.UtcNow,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId()
      }).ToList()
    };
  }
}
