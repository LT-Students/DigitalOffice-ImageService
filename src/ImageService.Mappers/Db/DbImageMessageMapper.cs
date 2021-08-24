using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageMessageMapper : IDbImageMessageMapper
    {
        public DbImagesMessage Map(CreateImageData createImageData, Guid? parentId = null, string content = null)
        {
            if (createImageData == null)
            {
                return null;
            }

            return new DbImagesMessage()
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = createImageData.Name,
                Content = content ?? createImageData.Content,
                Extension = createImageData.Extension,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = createImageData.CreatedBy
            };
        }
    }
}
