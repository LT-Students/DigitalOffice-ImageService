using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageNewsMapper : IDbImageNewsMapper
    {
        public DbImagesNews Map(CreateImageData createImageData, out Guid prewiewId)
        {
            prewiewId = Guid.NewGuid();

            return new DbImagesNews
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
        public List<DbImagesNews> Map(CreateImageData createImageData, string resizedContent, out Guid prewiewId)
        {
            List<DbImagesNews> result = new();
            Guid hqId = Guid.NewGuid();
            prewiewId = Guid.NewGuid();
            result.Add(
                new DbImagesNews()
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
                new DbImagesNews()
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
