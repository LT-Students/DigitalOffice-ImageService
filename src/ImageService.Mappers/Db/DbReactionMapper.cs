using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db;

public class DbReactionMapper : IDbReactionMapper
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public DbReactionMapper(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public DbReaction Map(CreateReactionRequest request)
  {
    return request is null
      ? null
      : new DbReaction
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Unicode = request.Unicode,
        Content = request.Content,
        Extension = request.Extension,
        GroupId = request.GroupId,
        IsActive = true,
        CreatedAtUtc = DateTime.UtcNow,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
      };
  }
}
