using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.Message;
using LT.DigitalOffice.Kernel.Attributes;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces
{
    [AutoInject]
    public interface IImageMessageResponseMapper
    {
        ImageMessageResponse Map(DbImageMessage dbImageMessage);
    }
}
