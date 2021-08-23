using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageNewsRepository
    {
        List<Guid> Create(List<DbImagesNews> imagesNews);
        List<DbImagesNews> Get(List<Guid> imageIds);
        DbImagesNews Get(Guid imageId);
        bool Delete(List<DbImagesNews> imagesNews);
    }
}
