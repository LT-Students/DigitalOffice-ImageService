using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.Data.SqlClient;

namespace LT.DigitalOffice.ImageService.Data
{
  public class ImageUserRepository : IImageUserRepository
  {
    private readonly IDataProvider _provider;

    public ImageUserRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public List<Guid> Create(List<DbImageUser> imagesUsers)
    {
      if (imagesUsers.Contains(null))
      {
        return null;
      }

      _provider.ImagesUsers.AddRange(imagesUsers);
      _provider.Save();

      return imagesUsers.Select(x => x.Id).ToList();
    }

    public bool Remove(List<Guid> imageIds)
    {
      if (imageIds == null)
      {
        return false;
      }

      SqlCommand command = new();

      foreach (Guid imageId in imageIds)
      {
        command.CommandText = $@"DELETE FROM {DbImageUser.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
            Id IN (SELECT ParentId FROM ImagesProjects WHERE Id = '{imageId}' AND ParentId IS NOT NULL);";

        _provider.ExecuteRawSql(command.CommandText);
      }

      return true;
    }

    public List<DbImageUser> Get(List<Guid> imageIds)
    {
      return _provider.ImagesUsers.Where(x => imageIds.Contains(x.Id)).ToList();
    }

    public DbImageUser Get(Guid imageId)
    {
      return _provider.ImagesUsers.FirstOrDefault(x => x.Id == imageId);
    }
  }
}
