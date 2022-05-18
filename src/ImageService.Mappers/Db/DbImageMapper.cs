using System;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Models.Broker.Models;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
  public class DbImageMapper : IDbImageMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DbImageMapper(
      IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public DbImage Map(
      CreateImageData request,
      Guid? parentId = null,
      string content = null,
      string extension = null)
    {
      return request is null
        ? null
        : new DbImage()
        {
          Id = Guid.NewGuid(),
          ParentId = parentId,
          Name = request.Name,
          Content = content ?? request.Content,
          Extension = extension ?? request.Extension,
          CreatedAtUtc = DateTime.UtcNow,
          CreatedBy = request.CreatedBy
        };
    }

    public DbImage Map(
      CreateImageRequest request,
      Guid? parentId = null,
      string content = null,
      string extension = null)
    {
      return request is null
        ? null
        : new DbImage()
        {
          Id = Guid.NewGuid(),
          ParentId = parentId,
          Name = request.Name,
          Content = content ?? request.Content,
          Extension = extension ?? request.Extension,
          CreatedAtUtc = DateTime.UtcNow,
          CreatedBy = _httpContextAccessor.HttpContext.GetUserId(),
        };
    }
  }
}
