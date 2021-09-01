using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.ImageService.Data
{
    public class ImageMessageRepository : IImageMessageRepository
    {
        private readonly IDataProvider _provider;
        public ImageMessageRepository(IDataProvider provider)
        {
            _provider = provider;
        }

        public List<Guid> Create(List<DbImageMessage> imagesMessages)
        {
            if (imagesMessages.Contains(null))
            {
                return null;
            }

            _provider.ImagesMessages.AddRange(imagesMessages);
            _provider.Save();

            return imagesMessages.Select(x => x.Id).ToList();
        }

        public List<DbImageMessage> Get(List<Guid> imageIds)
        {
            return _provider.ImagesMessages.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImageMessage Get(Guid imageId)
        {
            return _provider.ImagesMessages.FirstOrDefault(x => x.Id == imageId);
        }

        public bool Remove(List<Guid> imageIds)
        {
            if (imageIds == null)
            {
                return false;
            }

            SqlCommand command = new();
            string tableName = "ImagesMessages";

            foreach (Guid imageId in imageIds)
            {
                command.CommandText = $@"Delete From {tableName} where Id = '{imageId}' or ParentId = '{imageId}' or
            Id in (select ParentId from ImagesProjects where Id = '{imageId}' and ParentId is not null);";

                _provider.ExecuteRawSql(command.CommandText);
            }


            return true;
        }
    }
}
