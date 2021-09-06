using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Models;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
  public class DbImageProjectMapper : IDbImageProjectMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbImageProjectMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbImageProject Map(CreateImageData createImageData, Guid? parentId = null, string content = null)
    {
      if (createImageData == null)
      {
        return null;
      }

      return new DbImageProject
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
