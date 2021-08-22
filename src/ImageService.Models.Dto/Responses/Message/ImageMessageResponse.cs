using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Responses.Message
{
    public record ImageMessageResponse
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Extention { get; set; }
    }
}
