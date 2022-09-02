using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
  public class DbReactionGroupMapper : IDbReactionGroupMapper
  {
    private readonly IHttpContextAccessor _contextAccessor;

    public DbReactionGroupMapper(IHttpContextAccessor contextAccessor)
    {
      _contextAccessor = contextAccessor;
    }
    public DbReactionGroup Map(CreateReactionGroupRequest request)
    {
      return request is null
        ? null
        : new DbReactionGroup
        {
          Id = Guid.NewGuid(),
          Name = request.Name,
          IsActive = true,
          CreatedBy = _contextAccessor.HttpContext.GetUserId(),
          CreatedAtUtc = DateTime.UtcNow
        };
      /*if (request is null)
      {
        return null;
      }

      DbReactionGroup dbrg = new DbReactionGroup
      {
        Id = Guid.NewGuid(),
        Name = request.Name,
        IsActive = true,
        CreatedBy = _contextAccessor.HttpContext.GetUserId(),
        CreatedAtUtc = DateTime.UtcNow
      };
      return dbrg;*/
    }
  }
}
