using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;

namespace LT.DigitalOffice.ImageService.Data
{
  public class ImageNewsRepository : IImageNewsRepository
  {
    private readonly IDataProvider _provider;

    public ImageNewsRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public List<Guid> Create(List<DbImageNews> imagesNews)
    {
      if (imagesNews == null || !imagesNews.Any() || imagesNews.Contains(null))
      {
        return null;
      }

      _provider.ImagesNews.AddRange(imagesNews);
      _provider.Save();

      return imagesNews.Select(x => x.Id).ToList();
    }

    public bool Remove(List<Guid> imageIds)
    {
      if (imageIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imageIds)
      {
        _provider.ExecuteRawSql($@"DELETE FROM {DbImageNews.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
          Id IN (SELECT ParentId FROM ImagesProjects WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }

    public List<DbImageNews> Get(List<Guid> imageIds)
    {
      return _provider.ImagesNews.Where(x => imageIds.Contains(x.Id)).ToList();
    }

    public DbImageNews Get(Guid imageId)
    {
      return _provider.ImagesNews.FirstOrDefault(x => x.Id == imageId);
    }
  }
}
