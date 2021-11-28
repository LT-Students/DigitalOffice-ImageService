using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
  public class DbImageNewsMapper : IDbImageNewsMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbImageNewsMapper(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbImageNews Map(CreateImageRequest request, Guid? parentId = null, string content = null, string extension = null)
    {
      if (request == null)
      {
        return null;
      }

      return new DbImageNews
      {
        Id = Guid.NewGuid(),
        ParentId = parentId,
        Name = request.Name,
        Content = content ?? request.Content,
        Extension = extension ?? request.Extension,
        CreatedAtUtc = DateTime.UtcNow,
        CreatedBy = _httpContextAccessor.HttpContext.GetUserId()
      };
    }
  }
}
