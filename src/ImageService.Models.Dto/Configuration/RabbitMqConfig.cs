﻿using LT.DigitalOffice.Kernel.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Models.Dto.Configuration
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        public string CreateImagesNewsEndpoint { get; set; }
        public string GetImagesNewsEndpoint { get; set; }
        public string DeleteImagesNewsEndpoint { get; set; }
    }
}
