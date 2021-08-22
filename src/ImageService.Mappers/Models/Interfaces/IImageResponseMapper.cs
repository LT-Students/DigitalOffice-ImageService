using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Mappers.Models.Interfaces
{
    [AutoInject]
    public interface IImageResponseMapper
    {
        ImageDataResponse  Map(DbImagesProject dbImagesProject);
    }
}
