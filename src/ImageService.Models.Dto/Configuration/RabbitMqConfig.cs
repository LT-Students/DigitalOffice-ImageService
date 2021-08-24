using LT.DigitalOffice.Kernel.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesMessageEndpoint { get; set; }
        public string GetImagesMessageEndpoint { get; set; }
        public string DeleteImagesMessageEndpoint { get; set; }


    }
}
