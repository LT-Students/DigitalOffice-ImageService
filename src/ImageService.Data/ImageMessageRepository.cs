﻿using LT.DigitalOffice.ImageService.Data.Interfaces;
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

        public List<Guid> Create(List<DbImagesMessage> imagesMessages)
        {
            if (imagesMessages.Contains(null))
            {
                throw new ArgumentNullException(nameof(imagesMessages));
            }

            _provider.ImagesMessages.AddRange(imagesMessages);
            _provider.Save();

            return imagesMessages.Select(x => x.Id).ToList();
        }

        public List<DbImagesMessage> Get(List<Guid> imageIds)
        {
            return _provider.ImagesMessages.Where(x => imageIds.Contains(x.Id)).ToList();
        }

        public DbImagesMessage Get(Guid imageId)
        {
            return _provider.ImagesMessages.Where(x => x.Id == imageId).FirstOrDefault();
        }

        public bool Delete(List<DbImagesMessage> imagesMessages)
        {
            foreach (DbImagesMessage imageMessage in imagesMessages)
            {
                if (imageMessage == null)
                {
                    throw new ArgumentNullException(nameof(imagesMessages));
                }
            }

            _provider.ImagesMessages.RemoveRange(imagesMessages);
            _provider.Save();

            return true;
        }
    }
}
