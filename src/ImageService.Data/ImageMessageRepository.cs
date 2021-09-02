using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;

namespace LT.DigitalOffice.ImageService.Data
{
  public class ImageMessageRepository : IImageMessageRepository
  {
    private readonly IDataProvider _provider;
    public ImageMessageRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public List<Guid> Create(List<DbImageMessage> imagesMessages)
    {
      if (imagesMessages == null || !imagesMessages.Any() || imagesMessages.Contains(null))
      {
        return null;
      }

      _provider.ImagesMessages.AddRange(imagesMessages);
      _provider.Save();

      return imagesMessages.Select(x => x.Id).ToList();
    }

    public List<DbImageMessage> Get(List<Guid> imageIds)
    {
      return _provider.ImagesMessages.Where(x => imageIds.Contains(x.Id)).ToList();
    }

    public DbImageMessage Get(Guid imageId)
    {
      return _provider.ImagesMessages.FirstOrDefault(x => x.Id == imageId);
    }

    public bool Remove(List<Guid> imageIds)
    {
      if (imageIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imageIds)
      {
        _provider.ExecuteRawSql($@"DELETE FROM {DbImageMessage.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
            Id IN (SELECT ParentId FROM ImagesProjects WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }
  }
}
