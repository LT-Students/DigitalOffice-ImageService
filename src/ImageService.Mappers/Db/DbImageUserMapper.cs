using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageUserMapper : IDbImageUserMapper
    {
        public DbImagesUser Map(CreateImageData createImageData, Guid? parentId = null)
        {
            return new DbImagesUser()
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = createImageData.Name,
                Content = createImageData.Content,
                Extension = createImageData.Extension,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = createImageData.CreatedBy
            };
        }
    }
}
