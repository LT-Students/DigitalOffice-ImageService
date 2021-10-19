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
  public class ImageUserRepository : IImageUserRepository
  {
    private readonly IDataProvider _provider;

    public ImageUserRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> CreateAsync(List<DbImageUser> imagesUsers)
    {
      if (imagesUsers == null || !imagesUsers.Any() || imagesUsers.Contains(null))
      {
        return null;
      }

      _provider.ImagesUsers.AddRange(imagesUsers);
      await _provider.SaveAsync();

      return imagesUsers.Select(x => x.Id).ToList();
    }

    public async Task<bool> RemoveAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imagesIds)
      {
        await _provider.ExecuteRawSqlAsync($@"DELETE FROM {DbImageUser.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
          Id IN (SELECT ParentId FROM {DbImageUser.TableName} WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }

    public async Task<List<DbImageUser>> GetAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null || !imagesIds.Any())
      {
        return null;
      }

      return await _provider.ImagesUsers.Where(x => imagesIds.Contains(x.Id)).ToListAsync();
    }

    public async Task<DbImageUser> GetAsync(Guid imageId)
    {
      return await _provider.ImagesUsers.FirstOrDefaultAsync(x => x.Id == imageId);
    }
  }
}
