using LT.DigitalOffice.Kernel.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesUserEndpoint { get; set; }
        public string GetImagesUserEndpoint { get; set; }
        public string DeleteImagesUserEndpoint { get; set; }
    }
}
