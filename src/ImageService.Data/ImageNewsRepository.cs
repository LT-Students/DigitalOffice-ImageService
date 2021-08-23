using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ImageService.Data
{
    public class ImageNewsRepository : IImageNewsRepository
    {
        private readonly IDataProvider _provider;

        public ImageNewsRepository(IDataProvider provider)
        {
            _provider = provider;
        }

        public List<Guid> Create(List<DbImagesNews> imagesNews)
        {
            if (imagesNews.Contains(null))
            {
                throw new ArgumentNullException(nameof(imagesNews));
            }

            _provider.ImagesNews.AddRange(imagesNews);
            _provider.Save();

            return imagesNews.Select(x => x.Id).ToList();
        }

        public bool Delete(List<DbImagesNews> imagesNews)
        {
            if (imagesNews == null)
            {
                throw new ArgumentNullException(nameof(imagesNews));
            }

            _provider.ImagesNews.RemoveRange(imagesNews);
            _provider.Save();

            return true;
        }

        public List<DbImagesNews> Get(List<Guid> imageIds)
        {
            return _provider.ImagesNews.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImagesNews Get(Guid imageId)
        {
            return _provider.ImagesNews.Where(x => x.Id == imageId).FirstOrDefault();
        }
    }
}
