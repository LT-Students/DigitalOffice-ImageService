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

        public List<Guid> Create(List<DbImageNews> imagesNews)
        {
            if (imagesNews.Contains(null))
            {
                return null;
            }

            _provider.ImagesNews.AddRange(imagesNews);
            _provider.Save();

            return imagesNews.Select(x => x.Id).ToList();
        }

        public bool Remove(List<Guid> imageIds)
        {
            if (imageIds == null)
            {
                return false;
            }

            List<DbImageNews> imagesNews = _provider.ImagesNews
                .Where(x => imageIds.Contains(x.Id) || (x.ParentId != null && imageIds.Contains((Guid)x.ParentId)))
                .ToList();

            if (imagesNews == null)
            {
                return false;
            }

            List<Guid> parentIds = new();

            foreach (DbImageNews imageNews in imagesNews)
            {
                if (imageNews.ParentId != null
                    && imageNews.Id != imageNews.ParentId
                    && !imageIds.Contains((Guid)imageNews.ParentId))
                {
                    parentIds.Add((Guid)imageNews.ParentId);
                }
            }

            imagesNews.AddRange(_provider.ImagesNews.Where(x => parentIds.Contains(x.Id)));

            _provider.ImagesNews.RemoveRange(imagesNews);
            _provider.Save();

            return true;
        }

        public List<DbImageNews> Get(List<Guid> imageIds)
        {
            return _provider.ImagesNews.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImageNews Get(Guid imageId)
        {
            return _provider.ImagesNews.FirstOrDefault(x => x.Id == imageId);
        }
    }
}
