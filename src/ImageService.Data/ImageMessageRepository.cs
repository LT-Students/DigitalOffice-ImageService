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
                return null;
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

        public bool Delete(List<Guid> imageIds)
        {
            if (imageIds == null)
            {
                return false;
            }

            List<DbImagesMessage> imagesMessages = _provider.ImagesMessages
                .Where(x => imageIds.Contains(x.Id) || (x.ParentId != null && imageIds.Contains((Guid)x.ParentId)))
                .ToList();

            if (imagesMessages == null)
            {
                return false;
            }

            List<Guid> parentIds = new();

            foreach (DbImagesMessage imageMessage in imagesMessages)
            {
                if (imageMessage.ParentId != null
                    && imageMessage.Id != imageMessage.ParentId
                    && !imageIds.Contains((Guid)imageMessage.ParentId))
                {
                    parentIds.Add((Guid)imageMessage.ParentId);
                }
            }

            imagesMessages.AddRange(_provider.ImagesMessages.Where(x => parentIds.Contains(x.Id)));

            _provider.ImagesMessages.RemoveRange(imagesMessages);
            _provider.Save();

            return true;
        }
    }
}