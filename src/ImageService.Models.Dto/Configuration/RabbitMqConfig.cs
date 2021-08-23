using LT.DigitalOffice.Kernel.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesProjectEndpoint { get; set; }
        public string GetImagesProjectEndpoint { get; set; }
        public string DeleteImagesProjectEndpoint { get; set; }
    }
}
