using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
    [AutoInject]
    public interface IDbImageUserMapper
    {
        public DbImagesUser Map(Guid parentId, string name, string content, string extension, Guid createdBy);
    }
}
