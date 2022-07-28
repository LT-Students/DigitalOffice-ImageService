using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data
{
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
        case ImageSource.Reaction: return DBTablesNames.REACTION;
        default: throw new ArgumentOutOfRangeException();
      }
    }

    private IQueryable<DbImage> CreateFindReactionPredicates(
      FindReactionFilter filter,
      IQueryable<DbImage> dbReactionList)
    {
      if (filter.IsPreview.HasValue)
      {
        dbReactionList = filter.IsPreview.Value
          ? dbReactionList.Where(rl => rl.ParentId != null || rl.ParentId == rl.Id)
          : dbReactionList.Where(rl => rl.ParentId == null);
      }

      if (!string.IsNullOrEmpty(filter.NameIncludeSubstring))
      {
        dbReactionList = dbReactionList.Where(rl => rl.Name.Contains(filter.NameIncludeSubstring));
      }

      if (filter.IsAscendingSort.HasValue)
      {
        dbReactionList = filter.IsAscendingSort.Value
          ? dbReactionList.OrderBy(rl => rl.Name)
          : dbReactionList.OrderByDescending(rl => rl.Name);
      }

      return dbReactionList;
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

    public async Task<(List<DbImage> dbReactions, int totalCount)> FindReactionAsync(FindReactionFilter filter)
    {
      if (filter is null)
      {
        return (null, default);
      }

      IQueryable<DbImage> dbReactionList = CreateFindReactionPredicates(
        filter,
        _provider
        .FromSqlRaw($"SELECT * FROM {GetTargetDBTableName(ImageSource.Reaction)}")
            .OrderByDescending(x => x.CreatedAtUtc)
            .AsQueryable());

      return (
        await dbReactionList.Skip(filter.SkipCount).Take(filter.TakeCount).ToListAsync(),
        await dbReactionList.CountAsync());
    }

    public Task<bool> DoesSameNameExistAsync(string name, ImageSource source)
    {
      return _provider.FromSqlRaw($"SELECT * FROM {GetTargetDBTableName(source)}").AnyAsync(x => x.Name == name);
    }
  }
}
