﻿using LT.DigitalOffice.Kernel.Configurations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesProjectEndpoint { get; set; }
        public string GetImagesProjectEndpoint { get; set; }
        public string DeleteImagesProjectEndpoint { get; set; }
        public string CreateImagesMessageEndpoint { get; set; }
        public string GetImagesMessageEndpoint { get; set; }
        public string DeleteImagesMessageEndpoint { get; set; }
        public string CreateImagesNewsEndpoint { get; set; }
        public string GetImagesNewsEndpoint { get; set; }
        public string DeleteImagesNewsEndpoint { get; set; }
    }
}
