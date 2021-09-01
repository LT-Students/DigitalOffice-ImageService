using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.Data.SqlClient;
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

            SqlCommand command = new();
            string tableName = "ImagesProjects";

            foreach (Guid imageId in imageIds) {
                command.CommandText = $@"Delete From {tableName} where Id = '{imageId}' or ParentId = '{imageId}' or
            Id in (select ParentId from ImagesProjects where Id = '{imageId}' and ParentId is not null);";

                _provider.ExecuteRawSql(command.CommandText);
            }

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
