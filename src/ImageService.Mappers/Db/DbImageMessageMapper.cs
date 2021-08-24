using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageMessageMapper : IDbImageMessageMapper
    {
        public DbImagesMessage Map(CreateImageData createImageData, out Guid prewiewId)
        {
            prewiewId = Guid.NewGuid();

            return new DbImagesMessage()
            {
                Id = prewiewId,
                ParentId = prewiewId,
                Name = createImageData.Name,
                Content = createImageData.Content,
                Extension = createImageData.Extension,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = createImageData.CreatedBy
            };
        }

        public IEnumerable<DbImagesMessage> Map(CreateImageData createImageData, string resizedContent, out Guid prewiewId)
        {
            List<DbImagesMessage> result = new();
            Guid hqId = Guid.NewGuid();
            prewiewId = Guid.NewGuid();
            result.Add(
                new DbImagesMessage()
                {
                    Id = hqId,
                    ParentId = null,
                    Name = createImageData.Name,
                    Content = createImageData.Content,
                    Extension = createImageData.Extension,
                    CreatedAtUtc = DateTime.UtcNow,
                    CreatedBy = createImageData.CreatedBy
                });

            result.Add(
                new DbImagesMessage()
                {
                    Id = prewiewId,
                    ParentId = hqId,
                    Name = createImageData.Name,
                    Content = resizedContent,
                    Extension = createImageData.Extension,
                    CreatedAtUtc = DateTime.UtcNow,
                    CreatedBy = createImageData.CreatedBy
                });

            return result;
        }
    }
}
