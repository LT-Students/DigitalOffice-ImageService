using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;

namespace LT.DigitalOffice.ImageService.Data
{
  public class ImageProjectRepository : IImageProjectRepository
  {
    private readonly IDataProvider _provider;

    public ImageProjectRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public List<Guid> Create(List<DbImageProject> imagesProjects)
    {
      if (imagesProjects == null || !imagesProjects.Any() || imagesProjects.Contains(null))
      {
        return null;
      }

      _provider.ImagesProjects.AddRange(imagesProjects);
      _provider.Save();

      return imagesProjects.Select(x => x.Id).ToList();
    }

    public bool Remove(List<Guid> imageIds)
    {
      if (imageIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imageIds)
      {
        _provider.ExecuteRawSql($@"DELETE FROM {DbImageProject.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
          Id IN (SELECT ParentId FROM {DbImageProject.TableName} WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }

    public List<DbImageProject> Get(List<Guid> imageIds)
    {
      return _provider.ImagesProjects.Where(x => imageIds.Contains(x.Id)).ToList();
    }

    public DbImageProject Get(Guid imageId)
    {
      return _provider.ImagesProjects.FirstOrDefault(x => x.Id == imageId);
    }
  }
}
