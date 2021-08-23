using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageUserRepository
    {
        List<Guid> Create(List<DbImagesUser> imagesUsers);
        List<DbImagesUser> Get(List<Guid> imageIds);
        DbImagesUser Get(Guid imageId);
        bool Delete(List<DbImagesUser> imagesUsers);
    }
}
