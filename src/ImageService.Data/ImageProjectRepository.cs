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

        public List<Guid> Create(List<DbImagesProject> imagesProjects)
        {
            if (imagesProjects.Contains(null))
            {
                throw new ArgumentNullException(nameof(imagesProjects));
            }

            _provider.ImagesProjects.AddRange(imagesProjects);
            _provider.Save();

            return imagesProjects.Select(x => x.Id).ToList();
        }

        public bool Delete(List<DbImagesProject> imagesProjects)
        {
            if (imagesProjects == null)
            {
                return false;
            }

            _provider.ImagesProjects.RemoveRange(imagesProjects);
            _provider.Save();

            return true;
        }

        public List<DbImagesProject> Get(List<Guid> imageIds)
        {
            return _provider.ImagesProjects.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImagesProject Get(Guid imageId)
        {
            return _provider.ImagesProjects.Find(imageId);
        }
    }
}
