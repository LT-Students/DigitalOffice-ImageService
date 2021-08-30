using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageNewsRepository
    {
        List<Guid> Create(List<DbImageNews> imagesNews);

        List<DbImageNews> Get(List<Guid> imageIds);

        DbImageNews Get(Guid imageId);

        bool Remove(List<Guid> imageIds);
    }
}
