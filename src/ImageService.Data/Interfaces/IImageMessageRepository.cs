using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageMessageRepository
    {
        List<Guid> Create(List<DbImagesMessage> imagesMessages);

        List<DbImagesMessage> Get(List<Guid> imageIds);

        DbImagesMessage Get(Guid imageId);

        bool Delete(List<Guid> imageIds);
    }
}
