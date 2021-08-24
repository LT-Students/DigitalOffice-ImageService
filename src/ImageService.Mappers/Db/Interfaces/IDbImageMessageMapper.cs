using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
    [AutoInject]
    public interface IDbImageMessageMapper
    {
        DbImagesMessage Map(CreateImageData createImageData, out Guid prewiewId);
        IEnumerable<DbImagesMessage> Map(CreateImageData createImageData, string resizedContent, out Guid prewiewId);
    }
}
