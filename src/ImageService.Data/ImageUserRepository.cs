using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ImageService.Data
{
    public class ImageUserRepository : IImageUserRepository
    {
        private readonly IDataProvider _provider;

        public ImageUserRepository(IDataProvider provider)
        {
            _provider = provider;
        }

        public List<Guid> Create(List<DbImagesUser> imagesUsers)
        {
            if (imagesUsers.Contains(null))
            {
                throw new ArgumentNullException(nameof(imagesUsers));
            }

            _provider.ImagesUsers.AddRange(imagesUsers);
            _provider.Save();

            return imagesUsers.Select(x => x.Id).ToList();
        }

        public bool Delete(DbImagesUser imagesUsers)
        {
            if (imagesUsers == null)
            {
                throw new ArgumentNullException(nameof(imagesUsers));
            }

            _provider.ImagesUsers.Remove(imagesUsers);
            _provider.Save();

            return true;
        }

        public List<DbImagesUser> Get(List<Guid> imageIds)
        {
            return _provider.ImagesUsers.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImagesUser Get(Guid imageId)
        {
            return _provider.ImagesUsers.Where(x => x.Id == imageId).FirstOrDefault();
        }

    }
}
