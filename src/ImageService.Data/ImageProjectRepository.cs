using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (imagesProjects.Contains(null))
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

            List<DbImageProject> imagesProjects = _provider.ImagesProjects
                .Where(x => imageIds.Contains(x.Id) || (x.ParentId != null && imageIds.Contains((Guid)x.ParentId)))
                .ToList();

            if (imagesProjects == null)
            {
                return false;
            }

            List<Guid> parentIds = new();

            foreach (DbImageProject imageProject in imagesProjects)
            {
                if (imageProject.ParentId != null
                    && imageProject.Id != imageProject.ParentId
                    && !imageIds.Contains((Guid)imageProject.ParentId))
                {
                    parentIds.Add((Guid)imageProject.ParentId);
                }
            }

            imagesProjects.AddRange(_provider.ImagesProjects.Where(x => parentIds.Contains(x.Id)));

            _provider.ImagesProjects.RemoveRange(imagesProjects);
            _provider.Save();

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
