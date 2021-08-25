using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageDataResponseMapper : IImageDataResponseMapper
    {
        public ImageDataResponse Map(DbImagesProject dbImageProject)
        public ImageDataResponse Map(DbImagesMessage dbImageMessage)
        {
            if (dbImageProject == null)
            if (dbImageMessage == null)
            {
                return null;
            }

            return new ImageDataResponse
            {
                Id = dbImageProject.Id,
                Content = dbImageProject.Content,
                Name = dbImageProject.Name,
                Extension = dbImageProject.Extension
                Id = dbImageMessage.Id,
                Content = dbImageMessage.Content,
                Name = dbImageMessage.Name,
                Extension = dbImageMessage.Extension
            };
        }
    }
}
