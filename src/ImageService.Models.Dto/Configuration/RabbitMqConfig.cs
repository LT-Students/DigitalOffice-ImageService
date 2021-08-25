using LT.DigitalOffice.Kernel.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesMessageEndpoint { get; set; }
        public string GetImagesMessageEndpoint { get; set; }
        public string DeleteImagesMessageEndpoint { get; set; }
        public string CreateImagesNewsEndpoint { get; set; }
        public string GetImagesNewsEndpoint { get; set; }
        public string DeleteImagesNewsEndpoint { get; set; }
        public string CreateImagesUserEndpoint { get; set; }
        public string GetImagesUserEndpoint { get; set; }
        public string DeleteImagesUserEndpoint { get; set; }
    }
}
