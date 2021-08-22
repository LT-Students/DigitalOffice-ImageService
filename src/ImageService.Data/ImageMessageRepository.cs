using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Data.Provider;
using LT.DigitalOffice.ImageService.Models.Db;
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
            List<Guid> result = new();

            foreach(DbImageMessage imageMessage in imagesMessages)
            {
                if (imageMessage == null)
                {
                    throw new ArgumentNullException(nameof(imagesMessages));
                }
                else
                {
                    result.Add(imageMessage.Id);
                }
            }

            _provider.ImagesMessages.AddRange(imagesMessages);
            _provider.Save();

            return result;
        }

        public List<DbImageMessage> Get(List<Guid> imageIds)
        {
            return _provider.ImagesMessages.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImageMessage Get(Guid imageId)
        {
            return _provider.ImagesMessages.Where(x => x.Id == imageId).FirstOrDefault();
        }

        public List<bool> Delete(List<DbImageMessage> imagesMessages)
        {
            List<bool> result = new();

            foreach (DbImageMessage imageMessage in imagesMessages)
            {
                if (imageMessage == null)
                {
                    throw new ArgumentNullException(nameof(imagesMessages));
                }
                else
                {
                    result.Add(true);
                }
            }

            _provider.ImagesMessages.RemoveRange(imagesMessages);
            _provider.Save();

            return result;
        }
    }
}
