using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data;

public class ImageRepository : IImageRepository
{
  private readonly IDataProvider _provider;

  private string GetTargetDBTableName(ImageSource imageSource)
  {
    switch (imageSource)
    {
      case ImageSource.User: return DBTablesNames.USER;
      case ImageSource.Project: return DBTablesNames.PROJECT;
      case ImageSource.Message: return DBTablesNames.MESSAGE;
      case ImageSource.News: return DBTablesNames.NEWS;
      case ImageSource.Education: return DBTablesNames.EDUCATION;
      default: throw new ArgumentOutOfRangeException();
    }
  }

  public ImageRepository(IDataProvider provider)
  {
    _provider = provider;
  }

  public async Task CreateAsync(ImageSource sourse, List<DbImage> dbImages)
  {
    if (dbImages is not null && dbImages.Any())
    {
      string tableName = GetTargetDBTableName(sourse);

      string parentId = null;
      string name = null;

      foreach (DbImage image in dbImages)
      {
        parentId = image.ParentId is null ? "null" : $"'{image.ParentId}'";
        name = image.Name is null ? "null" : $"'{image.Name}'";

        await _provider.ExecuteRawSqlAsync(
          @$"INSERT INTO {tableName}
              (Id, ParentId, Name, Content, Extension, CreatedAtUtc, CreatedBy)
              VALUES ('{image.Id}', {parentId}, {name}, '{image.Content}', '{image.Extension}', '{image.CreatedAtUtc.ToString("yyyy-MM-dd HH:mm:ss")}', '{image.CreatedBy}')");
      }
    }
  }

  public Task<List<DbImage>> GetAsync(ImageSource sourse, List<Guid> imagesIds)
  {
    return imagesIds is null || !imagesIds.Any()
      ? null
      : _provider
        .FromSqlRaw($"SELECT * FROM {GetTargetDBTableName(sourse)}")
        .Where(x => imagesIds.Contains(x.Id))
        .ToListAsync();
  }

  public Task<DbImage> GetAsync(ImageSource sourse, Guid imageId)
  {
    return _provider
      .FromSqlRaw($"SELECT * FROM {GetTargetDBTableName(sourse)}")
      .FirstOrDefaultAsync(i => i.Id == imageId);
  }

  public async Task RemoveAsync(ImageSource sourse, List<Guid> imagesIds)
  {
    string tableName = GetTargetDBTableName(sourse);

    if (imagesIds is not null && imagesIds.Any())
    {
      foreach (Guid imageId in imagesIds)
      {
        await _provider.ExecuteRawSqlAsync(
          $@"DELETE FROM {tableName}
            WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR Id IN (
              SELECT ParentId
              FROM {tableName}
              WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }
    }
  }
}
