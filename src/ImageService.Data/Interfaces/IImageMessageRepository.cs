using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Data.Interfaces
{
    [AutoInject]
    public interface IImageMessageRepository
    {
        List<Guid> Create(List<DbImageMessage> imagesMessages);
        List<DbImageMessage> Get(List<Guid> imageIds);
        DbImageMessage Get(Guid imageId);
        List<bool> Delete(List<DbImageMessage> imagesMessages);
    }
}
