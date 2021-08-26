using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImageDataMapper : IImageDataMapper
    {
        public ImageData Map(DbImageProject imageProject)
        {
            if (imageProject == null)
            {
                return null;
            }

            return new ImageData(
                imageProject.Id,
                imageProject.ParentId,
                null,
                imageProject.Content,
                imageProject.Extension,
                imageProject.Name);
        }
    }
}
