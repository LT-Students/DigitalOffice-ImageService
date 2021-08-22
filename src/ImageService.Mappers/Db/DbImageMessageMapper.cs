using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db
{
    public class DbImageMessageMapper : IDbImageMessageMapper
    {
        public DbImageMessage Map(Guid parentId, string name, string content, string extention, Guid createdBy)
        {
            return new DbImageMessage()
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = name,
                Content = content,
                Extension = extention,
                CreatedAtUtc = DateTime.UtcNow,
                CreatedBy = createdBy
            };
        }
    }
}
