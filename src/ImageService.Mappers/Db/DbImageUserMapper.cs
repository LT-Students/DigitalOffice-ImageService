using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageUserMapper : IDbImageUserMapper
    {
        public DbImageUser Map(CreateImageData createImageData, Guid? parentId = null, string content = null)
        {
            if(createImageData == null)
            {
                return null;
            }

            return new DbImageUser()
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
