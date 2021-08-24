using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
    [AutoInject]
    public interface IDbImageNewsMapper
    {
        DbImagesNews Map(CreateImageData createImageData, Guid? parentId = null, string content = null);
    }
}
