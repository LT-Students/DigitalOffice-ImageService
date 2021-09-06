using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Models;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
  public class DbImageUserMapper : IDbImageUserMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbImageUserMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbImageUser Map(CreateImageData createImageData, Guid? parentId = null, string content = null)
    {
      if (createImageData == null)
      {
        return null;
      }

      return new DbImageUser()
      {
        Id = Guid.NewGuid(),
        ParentId = parentId,
        Name = createImageData.Name,
        Content = content ?? createImageData.Content,
        Extension = createImageData.Extension,
        CreatedAtUtc = DateTime.UtcNow,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId()
      };
    }
  }
}
