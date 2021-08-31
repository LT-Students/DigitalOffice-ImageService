using LT.DigitalOffice.Kernel.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesEndpoint { get; set; }
        public string GetImagesEndpoint { get; set; }
        public string RemoveImagesEndpoint { get; set; }
    }
}
