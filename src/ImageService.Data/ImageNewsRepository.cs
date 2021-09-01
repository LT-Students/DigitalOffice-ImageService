using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.Data.SqlClient;
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

            SqlCommand command = new();
            string tableName = "ImagesNews";

            foreach (Guid imageId in imageIds)
            {
                command.CommandText = $@"Delete From {tableName} where Id = '{imageId}' or ParentId = '{imageId}' or
            Id in (select ParentId from ImagesProjects where Id = '{imageId}' and ParentId is not null);";

                _provider.ExecuteRawSql(command.CommandText);
            }

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
