using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Responses.Message
{
    public class ImageMessageResponse
    {
        public Guid ImageId { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
    }
}
