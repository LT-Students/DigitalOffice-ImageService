using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageUserMapper : IDbImageUserMapper
    {
        public DbImagesUser Map(Guid parentId, string name, string content, string extension, Guid createdBy)
        {
            return new DbImagesUser
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = name,
                Content = content,
                Extension = extension,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }
    }
}
