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
  public class ImageMessageRepository : IImageMessageRepository
  {
    private readonly IDataProvider _provider;

    public ImageMessageRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> CreateAsync(List<DbImageMessage> imagesMessages)
    {
      if (imagesMessages == null || !imagesMessages.Any() || imagesMessages.Contains(null))
      {
        return null;
      }

      _provider.ImagesMessages.AddRange(imagesMessages);
      await _provider.SaveAsync();

      return imagesMessages.Select(x => x.Id).ToList();
    }

    public async Task<List<DbImageMessage>> GetAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null || !imagesIds.Any())
      {
        return null;
      }

      return await _provider.ImagesMessages.Where(x => imagesIds.Contains(x.Id)).ToListAsync();
    }

    public async Task<DbImageMessage> GetAsync(Guid imageId)
    {
      return await _provider.ImagesMessages.FirstOrDefaultAsync(x => x.Id == imageId);
    }

    public async Task<bool> RemoveAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imagesIds)
      {
        await _provider.ExecuteRawSqlAsync($@"DELETE FROM {DbImageMessage.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
          Id IN (SELECT ParentId FROM {DbImageMessage.TableName} WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }
  }
}
