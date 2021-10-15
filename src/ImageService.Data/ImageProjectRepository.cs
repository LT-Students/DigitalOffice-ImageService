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
  public class ImageProjectRepository : IImageProjectRepository
  {
    private readonly IDataProvider _provider;

    public ImageProjectRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<List<Guid>> CreateAsync(List<DbImageProject> imagesProjects)
    {
      if (imagesProjects == null || !imagesProjects.Any() || imagesProjects.Contains(null))
      {
        return null;
      }

      _provider.ImagesProjects.AddRange(imagesProjects);
      await _provider.SaveAsync();

      return imagesProjects.Select(x => x.Id).ToList();
    }

    public async Task<bool> RemoveAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null)
      {
        return false;
      }

      foreach (Guid imageId in imagesIds)
      {
        await _provider.ExecuteRawSqlAsync($@"DELETE FROM {DbImageProject.TableName} WHERE Id = '{imageId}' OR ParentId = '{imageId}' OR
          Id IN (SELECT ParentId FROM {DbImageProject.TableName} WHERE Id = '{imageId}' AND ParentId IS NOT NULL);");
      }

      return true;
    }

    public async Task<List<DbImageProject>> GetAsync(List<Guid> imagesIds)
    {
      if (imagesIds == null || !imagesIds.Any())
      {
        return null;
      }

      return await _provider.ImagesProjects.Where(x => imagesIds.Contains(x.Id)).ToListAsync();
    }

    public async Task<DbImageProject> GetAsync(Guid imageId)
    {
      return await _provider.ImagesProjects.FirstOrDefaultAsync(x => x.Id == imageId);
    }
  }
}
