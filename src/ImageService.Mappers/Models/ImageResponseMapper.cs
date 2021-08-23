using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Models
{
    public class ImageResponseMapper : IImageResponseMapper
    {
        public ImageDataResponse Map(DbImagesProject dbImageProject)
        {
            if (dbImageProject != null)
            {
                return new ImageDataResponse
                {
                    Id = dbImageProject.Id,
                    Content = dbImageProject.Content,
                    Name = dbImageProject.Name,
                    Extension = dbImageProject.Extension
                };
            }

            throw new ArgumentNullException(nameof(dbImageProject));
        }
    }
}
