using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.Message;
using System.Collections.Generic;

namespace LT.DigitalOffice.ImageService.Mappers.Responses
{
    public class ImageMessageResponseMapper : IImageMessageResponseMapper
    {
        public ImageMessageResponse Map(DbImageMessage dbImageMessage)
        {
            return new ImageMessageResponse
            {
                Id = dbImageMessage.Id,
                Content = dbImageMessage.Content,
                Name = dbImageMessage.Name,
                Extention = dbImageMessage.Extension
            };
        }
    }
}
