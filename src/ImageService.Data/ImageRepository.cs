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

namespace LT.DigitalOffice.ImageService.Data
{
  public class ImageRepository : IImageRepository
  {
    private readonly IDataProvider _provider;

    private string GetTargetDBTableName(ImageSource imageSource)
    {
      string tableName = String.Empty;

      switch (imageSource)
      {
        case ImageSource.User: tableName = DBTablesNames.USER; break;
        case ImageSource.Project: tableName = DBTablesNames.PROJECT; break;
        case ImageSource.Message: tableName = DBTablesNames.MESSAGE; break;
        default: throw new ArgumentOutOfRangeException();
      }

      return tableName;
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

        foreach (DbImage i in dbImages)
        {
          parentId = i.ParentId is null ? "null" : $"'{i.ParentId}'";
          name = i.Name is null ? "null" : $"'{i.Name}'";

          await _provider.ExecuteRawSqlAsync(
            @$"INSERT INTO {tableName}
              (Id, ParentId, Name, Content, Extension, CreatedAtUtc, CreatedBy)
              VALUES ('{i.Id}', {parentId}, {name}, '{i.Content}', '{i.Extension}', '{i.CreatedAtUtc}', '{i.CreatedBy}')");
        }
      }
    }

    public async Task<List<DbImage>> GetAsync(ImageSource sourse, List<Guid> imagesIds)
    {
      return imagesIds is null || !imagesIds.Any()
        ? null
        : await _provider
          .FromSqlRaw($"SELECT * FROM {GetTargetDBTableName(sourse)}")
          .Where(x => imagesIds.Contains(x.Id))
          .ToListAsync();
    }

    public async Task<DbImage> GetAsync(ImageSource sourse, Guid imageId)
    {
      return await _provider
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
}
