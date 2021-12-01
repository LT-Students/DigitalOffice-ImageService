using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    public string RemoveImagesEndpoint { get; set; }
    public string CreateImagesEndpoint { get; set; }
    public string GetImagesEndpoint { get; set; }
  }
}
