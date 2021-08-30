using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageUserRepository
    {
        List<Guid> Create(List<DbImageUser> imagesUsers);

        List<DbImageUser> Get(List<Guid> imageIds);

        DbImageUser Get(Guid imageId);

        bool Remove(List<Guid> imageIds);
    }
}
