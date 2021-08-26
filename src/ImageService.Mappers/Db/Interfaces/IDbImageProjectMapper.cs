using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Models;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Db.Interfaces
{
    [AutoInject]
    public interface IDbImageProjectMapper
    {
        DbImageProject Map(CreateImageData createImageData, Guid? parentId = null, string content = null);
    }
}
