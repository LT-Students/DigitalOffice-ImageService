using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.ImageService.Data
{
  public class ImageNewsRepository : IImageNewsRepository
  {
    private readonly IDataProvider _provider;

    public ImageNewsRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> CreateAsync(List<DbImageNews> imagesNews)
    {
      if (imagesNews == null || !imagesNews.Any() || imagesNews.Contains(null))
      {
        return null;
      }

      _provider.ImagesNews.AddRange(imagesNews);
      await _provider.SaveAsync();

      return imagesNews.Select(x => x.Id).ToList();
    }

    public async Task<bool> RemoveAsync(List<Guid> imageIds)
    {
      if (imageIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imageIds)
      {
        await _provider.ExecuteRawSqlAsync($@"DELETE FROM {DbImageNews.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
          Id IN (SELECT ParentId FROM {DbImageNews.TableName} WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }

    public async Task<List<DbImageNews>> GetAsync(List<Guid> imageIds)
    {
      return await _provider.ImagesNews.Where(x => imageIds.Contains(x.Id)).ToListAsync();
    }

    public async Task<DbImageNews> GetAsync(Guid imageId)
    {
      return await _provider.ImagesNews.FirstOrDefaultAsync(x => x.Id == imageId);
    }
  }
}
